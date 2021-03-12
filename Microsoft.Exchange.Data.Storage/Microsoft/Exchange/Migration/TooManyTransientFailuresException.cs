using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000154 RID: 340
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class TooManyTransientFailuresException : MigrationPermanentException
	{
		// Token: 0x0600160B RID: 5643 RVA: 0x0006ECCA File Offset: 0x0006CECA
		public TooManyTransientFailuresException(string batchIdentity) : base(Strings.ErrorTooManyTransientFailures(batchIdentity))
		{
			this.batchIdentity = batchIdentity;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0006ECDF File Offset: 0x0006CEDF
		public TooManyTransientFailuresException(string batchIdentity, Exception innerException) : base(Strings.ErrorTooManyTransientFailures(batchIdentity), innerException)
		{
			this.batchIdentity = batchIdentity;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x0006ECF5 File Offset: 0x0006CEF5
		protected TooManyTransientFailuresException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.batchIdentity = (string)info.GetValue("batchIdentity", typeof(string));
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0006ED1F File Offset: 0x0006CF1F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("batchIdentity", this.batchIdentity);
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x0006ED3A File Offset: 0x0006CF3A
		public string BatchIdentity
		{
			get
			{
				return this.batchIdentity;
			}
		}

		// Token: 0x04000AE3 RID: 2787
		private readonly string batchIdentity;
	}
}
