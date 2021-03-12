using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001064 RID: 4196
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServerToContactMustBeInSameDagException : LocalizedException
	{
		// Token: 0x0600B0C7 RID: 45255 RVA: 0x00296C2C File Offset: 0x00294E2C
		public ServerToContactMustBeInSameDagException(string serverToContact, string expectedDag, string actualDag) : base(Strings.ServerToContactMustBeInSameDagException(serverToContact, expectedDag, actualDag))
		{
			this.serverToContact = serverToContact;
			this.expectedDag = expectedDag;
			this.actualDag = actualDag;
		}

		// Token: 0x0600B0C8 RID: 45256 RVA: 0x00296C51 File Offset: 0x00294E51
		public ServerToContactMustBeInSameDagException(string serverToContact, string expectedDag, string actualDag, Exception innerException) : base(Strings.ServerToContactMustBeInSameDagException(serverToContact, expectedDag, actualDag), innerException)
		{
			this.serverToContact = serverToContact;
			this.expectedDag = expectedDag;
			this.actualDag = actualDag;
		}

		// Token: 0x0600B0C9 RID: 45257 RVA: 0x00296C78 File Offset: 0x00294E78
		protected ServerToContactMustBeInSameDagException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverToContact = (string)info.GetValue("serverToContact", typeof(string));
			this.expectedDag = (string)info.GetValue("expectedDag", typeof(string));
			this.actualDag = (string)info.GetValue("actualDag", typeof(string));
		}

		// Token: 0x0600B0CA RID: 45258 RVA: 0x00296CED File Offset: 0x00294EED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverToContact", this.serverToContact);
			info.AddValue("expectedDag", this.expectedDag);
			info.AddValue("actualDag", this.actualDag);
		}

		// Token: 0x17003854 RID: 14420
		// (get) Token: 0x0600B0CB RID: 45259 RVA: 0x00296D2A File Offset: 0x00294F2A
		public string ServerToContact
		{
			get
			{
				return this.serverToContact;
			}
		}

		// Token: 0x17003855 RID: 14421
		// (get) Token: 0x0600B0CC RID: 45260 RVA: 0x00296D32 File Offset: 0x00294F32
		public string ExpectedDag
		{
			get
			{
				return this.expectedDag;
			}
		}

		// Token: 0x17003856 RID: 14422
		// (get) Token: 0x0600B0CD RID: 45261 RVA: 0x00296D3A File Offset: 0x00294F3A
		public string ActualDag
		{
			get
			{
				return this.actualDag;
			}
		}

		// Token: 0x040061BA RID: 25018
		private readonly string serverToContact;

		// Token: 0x040061BB RID: 25019
		private readonly string expectedDag;

		// Token: 0x040061BC RID: 25020
		private readonly string actualDag;
	}
}
