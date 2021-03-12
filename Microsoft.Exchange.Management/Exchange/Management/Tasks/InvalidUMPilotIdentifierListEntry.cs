using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E84 RID: 3716
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidUMPilotIdentifierListEntry : LocalizedException
	{
		// Token: 0x0600A766 RID: 42854 RVA: 0x00288530 File Offset: 0x00286730
		public InvalidUMPilotIdentifierListEntry(string entryValue) : base(Strings.InvalidUMPilotIdentifierListEntry(entryValue))
		{
			this.entryValue = entryValue;
		}

		// Token: 0x0600A767 RID: 42855 RVA: 0x00288545 File Offset: 0x00286745
		public InvalidUMPilotIdentifierListEntry(string entryValue, Exception innerException) : base(Strings.InvalidUMPilotIdentifierListEntry(entryValue), innerException)
		{
			this.entryValue = entryValue;
		}

		// Token: 0x0600A768 RID: 42856 RVA: 0x0028855B File Offset: 0x0028675B
		protected InvalidUMPilotIdentifierListEntry(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.entryValue = (string)info.GetValue("entryValue", typeof(string));
		}

		// Token: 0x0600A769 RID: 42857 RVA: 0x00288585 File Offset: 0x00286785
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("entryValue", this.entryValue);
		}

		// Token: 0x17003673 RID: 13939
		// (get) Token: 0x0600A76A RID: 42858 RVA: 0x002885A0 File Offset: 0x002867A0
		public string EntryValue
		{
			get
			{
				return this.entryValue;
			}
		}

		// Token: 0x04005FD9 RID: 24537
		private readonly string entryValue;
	}
}
