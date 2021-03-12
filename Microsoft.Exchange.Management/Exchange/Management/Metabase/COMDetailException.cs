using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000DDC RID: 3548
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class COMDetailException : DataSourceOperationException
	{
		// Token: 0x0600A437 RID: 42039 RVA: 0x00283D2F File Offset: 0x00281F2F
		public COMDetailException(string errorMsg, string directoryEntry, string detailMessage) : base(Strings.COMDetailException(errorMsg, directoryEntry, detailMessage))
		{
			this.errorMsg = errorMsg;
			this.directoryEntry = directoryEntry;
			this.detailMessage = detailMessage;
		}

		// Token: 0x0600A438 RID: 42040 RVA: 0x00283D54 File Offset: 0x00281F54
		public COMDetailException(string errorMsg, string directoryEntry, string detailMessage, Exception innerException) : base(Strings.COMDetailException(errorMsg, directoryEntry, detailMessage), innerException)
		{
			this.errorMsg = errorMsg;
			this.directoryEntry = directoryEntry;
			this.detailMessage = detailMessage;
		}

		// Token: 0x0600A439 RID: 42041 RVA: 0x00283D7C File Offset: 0x00281F7C
		protected COMDetailException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
			this.directoryEntry = (string)info.GetValue("directoryEntry", typeof(string));
			this.detailMessage = (string)info.GetValue("detailMessage", typeof(string));
		}

		// Token: 0x0600A43A RID: 42042 RVA: 0x00283DF1 File Offset: 0x00281FF1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMsg", this.errorMsg);
			info.AddValue("directoryEntry", this.directoryEntry);
			info.AddValue("detailMessage", this.detailMessage);
		}

		// Token: 0x170035E4 RID: 13796
		// (get) Token: 0x0600A43B RID: 42043 RVA: 0x00283E2E File Offset: 0x0028202E
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x170035E5 RID: 13797
		// (get) Token: 0x0600A43C RID: 42044 RVA: 0x00283E36 File Offset: 0x00282036
		public string DirectoryEntry
		{
			get
			{
				return this.directoryEntry;
			}
		}

		// Token: 0x170035E6 RID: 13798
		// (get) Token: 0x0600A43D RID: 42045 RVA: 0x00283E3E File Offset: 0x0028203E
		public string DetailMessage
		{
			get
			{
				return this.detailMessage;
			}
		}

		// Token: 0x04005F4A RID: 24394
		private readonly string errorMsg;

		// Token: 0x04005F4B RID: 24395
		private readonly string directoryEntry;

		// Token: 0x04005F4C RID: 24396
		private readonly string detailMessage;
	}
}
