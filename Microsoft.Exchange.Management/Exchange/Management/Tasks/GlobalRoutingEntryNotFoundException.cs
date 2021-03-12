using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E81 RID: 3713
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GlobalRoutingEntryNotFoundException : LocalizedException
	{
		// Token: 0x0600A757 RID: 42839 RVA: 0x002883C8 File Offset: 0x002865C8
		public GlobalRoutingEntryNotFoundException(string phoneNumber) : base(Strings.GlobalRoutingEntryNotFound(phoneNumber))
		{
			this.phoneNumber = phoneNumber;
		}

		// Token: 0x0600A758 RID: 42840 RVA: 0x002883DD File Offset: 0x002865DD
		public GlobalRoutingEntryNotFoundException(string phoneNumber, Exception innerException) : base(Strings.GlobalRoutingEntryNotFound(phoneNumber), innerException)
		{
			this.phoneNumber = phoneNumber;
		}

		// Token: 0x0600A759 RID: 42841 RVA: 0x002883F3 File Offset: 0x002865F3
		protected GlobalRoutingEntryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.phoneNumber = (string)info.GetValue("phoneNumber", typeof(string));
		}

		// Token: 0x0600A75A RID: 42842 RVA: 0x0028841D File Offset: 0x0028661D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("phoneNumber", this.phoneNumber);
		}

		// Token: 0x17003670 RID: 13936
		// (get) Token: 0x0600A75B RID: 42843 RVA: 0x00288438 File Offset: 0x00286638
		public string PhoneNumber
		{
			get
			{
				return this.phoneNumber;
			}
		}

		// Token: 0x04005FD6 RID: 24534
		private readonly string phoneNumber;
	}
}
