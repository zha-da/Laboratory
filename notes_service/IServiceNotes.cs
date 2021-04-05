using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace notes_service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceNotes" in both code and config file together.
    [ServiceContract]
    public interface IServiceNotes
    {
        [OperationContract]
        Note CreateNote(string text);

        [OperationContract]
        List<Note> ConnectToService(string username);

        [OperationContract(IsOneWay = false)]
        void DisconnectFromService(string username, List<Note> notes);
    }
}
