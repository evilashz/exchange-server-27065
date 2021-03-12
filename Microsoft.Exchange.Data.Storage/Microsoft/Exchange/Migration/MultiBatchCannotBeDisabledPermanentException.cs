using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200014D RID: 333
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MultiBatchCannotBeDisabledPermanentException : MigrationPermanentException
	{
		// Token: 0x060015EC RID: 5612 RVA: 0x0006EAA1 File Offset: 0x0006CCA1
		public MultiBatchCannotBeDisabledPermanentException(string feature) : base(Strings.FeatureCannotBeDisabled(feature))
		{
			this.feature = feature;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x0006EAB6 File Offset: 0x0006CCB6
		public MultiBatchCannotBeDisabledPermanentException(string feature, Exception innerException) : base(Strings.FeatureCannotBeDisabled(feature), innerException)
		{
			this.feature = feature;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0006EACC File Offset: 0x0006CCCC
		protected MultiBatchCannotBeDisabledPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.feature = (string)info.GetValue("feature", typeof(string));
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0006EAF6 File Offset: 0x0006CCF6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("feature", this.feature);
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x0006EB11 File Offset: 0x0006CD11
		public string Feature
		{
			get
			{
				return this.feature;
			}
		}

		// Token: 0x04000AE0 RID: 2784
		private readonly string feature;
	}
}
