using System;
using System.Collections.Generic;
using System.Text;
using Goodware.Jabber.Library;
namespace Goodware.Jabber.Client
{
    /// <summary>
    /// ������� �� IQ ������
    /// </summary>
    public class IQHandler : PacketListener
    {
        JabberModel jabberModel;
        public IQHandler(JabberModel model) {
            jabberModel = model;
        }
        public void notify(Packet packet)
        {
            if (jabberModel == null)
            {
                Console.WriteLine("IQHandler Error - jabberModel==null");
                /// TODO �� ������ ����� �����!?
                /// jabberModel = JabberModel.getModel();
            }
            if (packet.ID != null)
            {
                PacketListener listener = jabberModel.removeResultHandler(packet.ID);
                if (listener != null)
                {
                    listener.notify(packet);
                    return;
                }
            }
            
        }
    }
}
