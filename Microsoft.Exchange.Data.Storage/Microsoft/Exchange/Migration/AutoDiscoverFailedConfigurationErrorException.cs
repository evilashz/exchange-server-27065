using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000192 RID: 402
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AutoDiscoverFailedConfigurationErrorException : MigrationPermanentException
	{
		// Token: 0x06001728 RID: 5928 RVA: 0x0007031B File Offset: 0x0006E51B
		public AutoDiscoverFailedConfigurationErrorException(LocalizedString details) : base(Strings.AutoDiscoverConfigurationError(details))
		{
			this.details = details;
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x00070330 File Offset: 0x0006E530
		public AutoDiscoverFailedConfigurationErrorException(LocalizedString details, Exception innerException) : base(Strings.AutoDiscoverConfigurationError(details), innerException)
		{
			this.details = details;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00070346 File Offset: 0x0006E546
		protected AutoDiscoverFailedConfigurationErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.details = (LocalizedString)info.GetValue("details", typeof(LocalizedString));
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x00070370 File Offset: 0x0006E570
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("details", this.details);
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600172C RID: 5932 RVA: 0x00070390 File Offset: 0x0006E590
		public LocalizedString Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x04000B08 RID: 2824
		private readonly LocalizedString details;
	}
}
