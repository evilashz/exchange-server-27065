using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000155 RID: 341
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CouldNotAddExchangeSnapInTransientException : MigrationTransientException
	{
		// Token: 0x06001610 RID: 5648 RVA: 0x0006ED42 File Offset: 0x0006CF42
		public CouldNotAddExchangeSnapInTransientException(string snapInName) : base(Strings.CouldNotAddExchangeSnapIn(snapInName))
		{
			this.snapInName = snapInName;
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0006ED57 File Offset: 0x0006CF57
		public CouldNotAddExchangeSnapInTransientException(string snapInName, Exception innerException) : base(Strings.CouldNotAddExchangeSnapIn(snapInName), innerException)
		{
			this.snapInName = snapInName;
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0006ED6D File Offset: 0x0006CF6D
		protected CouldNotAddExchangeSnapInTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.snapInName = (string)info.GetValue("snapInName", typeof(string));
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0006ED97 File Offset: 0x0006CF97
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("snapInName", this.snapInName);
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x0006EDB2 File Offset: 0x0006CFB2
		public string SnapInName
		{
			get
			{
				return this.snapInName;
			}
		}

		// Token: 0x04000AE4 RID: 2788
		private readonly string snapInName;
	}
}
