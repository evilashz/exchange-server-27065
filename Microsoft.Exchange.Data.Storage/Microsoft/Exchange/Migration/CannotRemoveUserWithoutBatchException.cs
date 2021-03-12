using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200015A RID: 346
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotRemoveUserWithoutBatchException : MigrationPermanentException
	{
		// Token: 0x06001628 RID: 5672 RVA: 0x0006EF51 File Offset: 0x0006D151
		public CannotRemoveUserWithoutBatchException(string userName) : base(Strings.ErrorCannotRemoveUserWithoutBatch(userName))
		{
			this.userName = userName;
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0006EF66 File Offset: 0x0006D166
		public CannotRemoveUserWithoutBatchException(string userName, Exception innerException) : base(Strings.ErrorCannotRemoveUserWithoutBatch(userName), innerException)
		{
			this.userName = userName;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0006EF7C File Offset: 0x0006D17C
		protected CannotRemoveUserWithoutBatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userName = (string)info.GetValue("userName", typeof(string));
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0006EFA6 File Offset: 0x0006D1A6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userName", this.userName);
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x0006EFC1 File Offset: 0x0006D1C1
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x04000AE8 RID: 2792
		private readonly string userName;
	}
}
