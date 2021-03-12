using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000191 RID: 401
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AutoDiscoverFailedInternalErrorException : MigrationPermanentException
	{
		// Token: 0x06001723 RID: 5923 RVA: 0x0007029E File Offset: 0x0006E49E
		public AutoDiscoverFailedInternalErrorException(LocalizedString details) : base(Strings.AutoDiscoverInternalError(details))
		{
			this.details = details;
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000702B3 File Offset: 0x0006E4B3
		public AutoDiscoverFailedInternalErrorException(LocalizedString details, Exception innerException) : base(Strings.AutoDiscoverInternalError(details), innerException)
		{
			this.details = details;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x000702C9 File Offset: 0x0006E4C9
		protected AutoDiscoverFailedInternalErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.details = (LocalizedString)info.GetValue("details", typeof(LocalizedString));
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x000702F3 File Offset: 0x0006E4F3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("details", this.details);
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x00070313 File Offset: 0x0006E513
		public LocalizedString Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x04000B07 RID: 2823
		private readonly LocalizedString details;
	}
}
