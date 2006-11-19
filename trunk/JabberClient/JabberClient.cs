using System;
using System.Collections.Generic;
using System.Text;

namespace Goodware.Jabber.Client {
	class JabberClient {
		String jabberVersion = "v. 1.0 - ch. 4";
		String sName;
		String sAddress;
		String sPort;
		String user;
		String resource;

		// Program version
		public String Version {
			get {
				return jabberVersion;
			}
		}

		// Server name
		public String ServerName {
			get {
				return sName;
			}
			set {
				sName = value;
			}
		}

		// Server Address
		public String ServerAddress {
			get {
				return sAddress;
			}
			set {
				sAddress = value;
			}
		}

		// Port
		public String Port {
			get {
				return sPort;
			}
			set {
				sPort = value;
			}
		}

		// User name
		public String User {
			get {
				return user;
			}
			set {
				user = value;
			}
		}

		// Resource (JustTalk)
		public String Resource {
			get {
				return resource;
			}
			set {
				resource = value;
			}
		}

		// Connect to a server (direct way)
		// parameters: server address, port, server name, user name, resource
		public void connect(String server, int port, String serverName, String user, String resource) {
		}

		// Disconnect from server
		public void disconnect() {
		}

		// Send message to user
		// Parameters:
		//      recipient (Jabber ID)
		//      Subject
		//      Thread
		//      Type
		//      ID
		//      Body
		// ToBe decyphered
		public void sendMessage(string recipient, string subject, string thread, string type, string id, string body) {
		}

		// Update presence
		// Parametres:
		//      status - ex. "away"
		//      display - ex. "not here"
		public void changePresence(String status, String display) {
			// Äåëåãèðàíî: changePresence(null, "away", "ne sum na kompjuter", null);
		}

		// Register to a server with the data
		public void register() {
		}

		// Remove a roster
		public void RemoveRoster(String jid) {
		}

		// Ask for rosters (asynhronous)
		public void sendRosterGet() {
		}

		// Change/add roster info
		// Parametres:
		//      jid - the jid to be changed
		//      name - custom nickname for the jid
		//      groups - groups in which the jid should be a member
		public void SetRoster(String jid, String name, String[] groups)//hashtable type may be changed
		{
		}

	}
}