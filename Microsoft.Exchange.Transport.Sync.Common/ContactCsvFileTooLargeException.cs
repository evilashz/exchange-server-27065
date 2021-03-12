using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000018 RID: 24
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ContactCsvFileTooLargeException : ImportContactsException
	{
		// Token: 0x06000122 RID: 290 RVA: 0x00004FA0 File Offset: 0x000031A0
		public ContactCsvFileTooLargeException(int maxContacts) : base(Strings.ContactCsvFileTooLarge(maxContacts))
		{
			this.maxContacts = maxContacts;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004FB5 File Offset: 0x000031B5
		public ContactCsvFileTooLargeException(int maxContacts, Exception innerException) : base(Strings.ContactCsvFileTooLarge(maxContacts), innerException)
		{
			this.maxContacts = maxContacts;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004FCB File Offset: 0x000031CB
		protected ContactCsvFileTooLargeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.maxContacts = (int)info.GetValue("maxContacts", typeof(int));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004FF5 File Offset: 0x000031F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("maxContacts", this.maxContacts);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00005010 File Offset: 0x00003210
		public int MaxContacts
		{
			get
			{
				return this.maxContacts;
			}
		}

		// Token: 0x040000D9 RID: 217
		private readonly int maxContacts;
	}
}
