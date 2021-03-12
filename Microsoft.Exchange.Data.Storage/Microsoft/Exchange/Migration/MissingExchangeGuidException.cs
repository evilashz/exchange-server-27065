using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000152 RID: 338
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MissingExchangeGuidException : MigrationPermanentException
	{
		// Token: 0x06001601 RID: 5633 RVA: 0x0006EBD5 File Offset: 0x0006CDD5
		public MissingExchangeGuidException(string identity) : base(Strings.ErrorMissingExchangeGuid(identity))
		{
			this.identity = identity;
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0006EBEA File Offset: 0x0006CDEA
		public MissingExchangeGuidException(string identity, Exception innerException) : base(Strings.ErrorMissingExchangeGuid(identity), innerException)
		{
			this.identity = identity;
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0006EC00 File Offset: 0x0006CE00
		protected MissingExchangeGuidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.identity = (string)info.GetValue("identity", typeof(string));
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0006EC2A File Offset: 0x0006CE2A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("identity", this.identity);
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x0006EC45 File Offset: 0x0006CE45
		public string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04000AE1 RID: 2785
		private readonly string identity;
	}
}
