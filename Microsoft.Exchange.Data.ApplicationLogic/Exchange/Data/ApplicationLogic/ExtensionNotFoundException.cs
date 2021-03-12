using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x02000007 RID: 7
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ExtensionNotFoundException : OwaExtensionOperationException
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00003470 File Offset: 0x00001670
		public ExtensionNotFoundException(string extensionID) : base(Strings.ErrorExtensionNotFound(extensionID))
		{
			this.extensionID = extensionID;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003485 File Offset: 0x00001685
		public ExtensionNotFoundException(string extensionID, Exception innerException) : base(Strings.ErrorExtensionNotFound(extensionID), innerException)
		{
			this.extensionID = extensionID;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000349B File Offset: 0x0000169B
		protected ExtensionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.extensionID = (string)info.GetValue("extensionID", typeof(string));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000034C5 File Offset: 0x000016C5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("extensionID", this.extensionID);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000034E0 File Offset: 0x000016E0
		public string ExtensionID
		{
			get
			{
				return this.extensionID;
			}
		}

		// Token: 0x04000065 RID: 101
		private readonly string extensionID;
	}
}
