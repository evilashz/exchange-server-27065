using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200019F RID: 415
	[Serializable]
	internal class SimpleServerSettings : ADServerSettings, ICloneable, IEquatable<SimpleServerSettings>
	{
		// Token: 0x060011A9 RID: 4521 RVA: 0x00055A68 File Offset: 0x00053C68
		internal SimpleServerSettings()
		{
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060011AA RID: 4522 RVA: 0x00055A70 File Offset: 0x00053C70
		protected override bool EnforceIsUpdatableByADSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x00055A73 File Offset: 0x00053C73
		// (set) Token: 0x060011AC RID: 4524 RVA: 0x00055A76 File Offset: 0x00053C76
		internal override ADObjectId RecipientViewRoot
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotSupportedException("RecipientViewRoot setter is not supported on SimpleServerSettings");
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x00055A82 File Offset: 0x00053C82
		// (set) Token: 0x060011AE RID: 4526 RVA: 0x00055A85 File Offset: 0x00053C85
		internal override bool ViewEntireForest
		{
			get
			{
				return true;
			}
			set
			{
				throw new NotSupportedException("ViewEntireForest setter is not supported on SimpleServerSettings");
			}
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00055A94 File Offset: 0x00053C94
		public override object Clone()
		{
			SimpleServerSettings simpleServerSettings = new SimpleServerSettings();
			this.CopyTo(simpleServerSettings);
			return simpleServerSettings;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x00055AAF File Offset: 0x00053CAF
		public bool Equals(SimpleServerSettings other)
		{
			return base.Equals(other);
		}
	}
}
