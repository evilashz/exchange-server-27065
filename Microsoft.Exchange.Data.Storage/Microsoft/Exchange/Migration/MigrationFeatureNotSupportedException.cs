using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000147 RID: 327
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationFeatureNotSupportedException : MigrationPermanentException
	{
		// Token: 0x060015CE RID: 5582 RVA: 0x0006E7C9 File Offset: 0x0006C9C9
		public MigrationFeatureNotSupportedException(string feature) : base(Strings.MigrationFeatureNotSupported(feature))
		{
			this.feature = feature;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0006E7DE File Offset: 0x0006C9DE
		public MigrationFeatureNotSupportedException(string feature, Exception innerException) : base(Strings.MigrationFeatureNotSupported(feature), innerException)
		{
			this.feature = feature;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0006E7F4 File Offset: 0x0006C9F4
		protected MigrationFeatureNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.feature = (string)info.GetValue("feature", typeof(string));
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0006E81E File Offset: 0x0006CA1E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("feature", this.feature);
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060015D2 RID: 5586 RVA: 0x0006E839 File Offset: 0x0006CA39
		public string Feature
		{
			get
			{
				return this.feature;
			}
		}

		// Token: 0x04000ADA RID: 2778
		private readonly string feature;
	}
}
