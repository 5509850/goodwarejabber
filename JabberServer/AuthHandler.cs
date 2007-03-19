using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;


namespace Goodware.Jabber.Server
{
    /// <summary>
    ///  ������� �� ������������
    /// UserIndex � User ������� ���� �� �� ���������, �� �� ��� �������� ����� �� ���.
    /// �� ����� ����� �������� get � set, ��� �� ������ � property �� ����� �� �� ���������
    /// �����, �� ����� ��� ������ :)
    /// </summary>
    public class AuthHandler : PacketListener
    {
        
        static UserIndex userIndex;
        Packet iq;
        Packet reply;
        User user;
        
        String username;
        String resource;
        Session session;
        Authenticator auth;

        
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="userindex"></param>
        public AuthHandler(UserIndex userindex)
        {
            userIndex = userindex;
            iq = new Packet("iq");
            auth = new Authenticator();
        }
        

        /// <summary>
        /// �������� ��� - ��������� �� ������
        /// </summary>
        /// <param name="packet"></param>
        public void notify(Packet packet)
        {
		
            String type = packet.Type;
            Packet query = packet.getFirstChild("query");
            username = query.getChildValue("username");
            iq.setID(packet.getID());
            iq.Session = packet.Session;
            iq.getChildren().Clear();
            iq.Type = "result";

            reply = new Packet("query");
            reply.setAttribute("xmlns", "jabber:iq:auth");
            reply.Parent = iq;

            user = userIndex.getUser(username);
            if (user == null)
            {
                sendErrorPacket(404, "User not found");
                return;
            }

            if (userIndex.sessionIndex.ContainsValue(user) == true)
            {
                sendErrorPacket(404, "User already Logged In");
                return;
            }
            

            if (type.Equals("get"))
            {
                sendGetPacket();
                return;
            }
            else if (type.Equals("set"))
            {
                session = packet.Session;
					 if (session.Status != Session.SessionStatus.streaming) {
						 sendErrorPacket(400, "Server name does not match");
						 return;
					 }
                resource = query.getChildValue("resource");
                if (resource == null)
                {
                    sendErrorPacket(404, "You must send resource");
                    return;
                }
                handleSetPacket(query);
                return;
            }
        }


        void sendErrorPacket(int code, String message)
        {
            iq.Type = "error";
            ErrorTool.setError(reply, code, message);
            MessageHandler.deliverPacket(iq);
        }


        void sendGetPacket()
        {
            new Packet("username", username).Parent = reply;
            new Packet("resource").Parent = reply;
            new Packet("password").Parent = reply;
            new Packet("digest").Parent = reply;
            new Packet("sequence", user.getSequence()).Parent = reply;
            new Packet("token", user.getToken()).Parent = reply;
            MessageHandler.deliverPacket(iq);
        }
        

        void handleSetPacket(Packet query){
            String password = query.getChildValue("password");
            String digest = query.getChildValue("digest");
            String hash = query.getChildValue("hash");
            if (password != null){
                if (user.getPassword().Equals(password)){
                    authenticated();
                    return;
                }
            } else if (digest != null){
                if (auth.isDigestAuthenticated(session.StreamID,password,digest)){
                    authenticated();
                    return;
                }
            } else if (hash != null){
                if (auth.isHashAuthenticated(user.getHash(),hash)){
                    user.setHash(hash);
                    // ������������ �� �����
                    int newSeq = int.Parse(user.getSequence()) - 1;

                    user.setSequence(   newSeq.ToString() );
                    // ���
                    authenticated();
                    return;
                }
            }
            sendErrorPacket(401,"Bad user name or password");
        }
        
        // TODO: SERVERNAME ����� �� ���� �� �������� ����������, ���� � hard-coded
        void authenticated()
        {
            MessageHandler.deliverPacket(iq);
            session.JID = new JabberID(username, JabberServer.server_name, resource);
            session.Status = Session.SessionStatus.authenticated;
            userIndex.addSession(session);
        }
    }
}
