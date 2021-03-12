using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011E8 RID: 4584
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DuplicateE164PilotIdentifierListEntryException : LocalizedException
	{
		// Token: 0x0600B968 RID: 47464 RVA: 0x002A5ED5 File Offset: 0x002A40D5
		public DuplicateE164PilotIdentifierListEntryException(string objectName) : base(Strings.DuplicateE164PilotIdentifierListEntry(objectName))
		{
			this.objectName = objectName;
		}

		// Token: 0x0600B969 RID: 47465 RVA: 0x002A5EEA File Offset: 0x002A40EA
		public DuplicateE164PilotIdentifierListEntryException(string objectName, Exception innerException) : base(Strings.DuplicateE164PilotIdentifierListEntry(objectName), innerException)
		{
			this.objectName = objectName;
		}

		// Token: 0x0600B96A RID: 47466 RVA: 0x002A5F00 File Offset: 0x002A4100
		protected DuplicateE164PilotIdentifierListEntryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectName = (string)info.GetValue("objectName", typeof(string));
		}

		// Token: 0x0600B96B RID: 47467 RVA: 0x002A5F2A File Offset: 0x002A412A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectName", this.objectName);
		}

		// Token: 0x17003A45 RID: 14917
		// (get) Token: 0x0600B96C RID: 47468 RVA: 0x002A5F45 File Offset: 0x002A4145
		public string ObjectName
		{
			get
			{
				return this.objectName;
			}
		}

		// Token: 0x04006460 RID: 25696
		private readonly string objectName;
	}
}
