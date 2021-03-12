using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public class PSRemoteServer : ExchangeDataObject
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000AB80 File Offset: 0x00008D80
		internal override ObjectSchema Schema
		{
			get
			{
				return PSRemoteServer.schema;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000AB8F File Offset: 0x00008D8F
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000ABA1 File Offset: 0x00008DA1
		public Fqdn RemotePSServer
		{
			get
			{
				return (Fqdn)this[PSRemoteServerSchema.RemotePSServer];
			}
			set
			{
				this[PSRemoteServerSchema.RemotePSServer] = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000ABAF File Offset: 0x00008DAF
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000ABB7 File Offset: 0x00008DB7
		public string UserAccount { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		public string DisplayName { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000ABD1 File Offset: 0x00008DD1
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000ABD9 File Offset: 0x00008DD9
		public bool AutomaticallySelect { get; set; }

		// Token: 0x060002F4 RID: 756 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		public override ValidationError[] Validate()
		{
			List<ValidationError> list = new List<ValidationError>();
			if (!this.AutomaticallySelect && this.RemotePSServer == null)
			{
				list.Add(new PropertyValidationError(Strings.ManuallySelectedServerEmpty, PSRemoteServerSchema.RemotePSServer, this));
			}
			return list.ToArray();
		}

		// Token: 0x040000C9 RID: 201
		private static PSRemoteServerSchema schema = ObjectSchema.GetInstance<PSRemoteServerSchema>();
	}
}
