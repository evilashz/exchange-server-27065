using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000A7 RID: 167
	internal class SqlDBVersion : ConfigurablePropertyBag
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00012960 File Offset: 0x00010B60
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.VersionId.ToString());
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00012986 File Offset: 0x00010B86
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x00012998 File Offset: 0x00010B98
		public Guid VersionId
		{
			get
			{
				return (Guid)this[SqlDBVersion.VersionIdProp];
			}
			set
			{
				this[SqlDBVersion.VersionIdProp] = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x000129AB File Offset: 0x00010BAB
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x000129BD File Offset: 0x00010BBD
		public string VersionName
		{
			get
			{
				return (string)this[SqlDBVersion.VersionStringProp];
			}
			set
			{
				this[SqlDBVersion.VersionStringProp] = value;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x000129CB File Offset: 0x00010BCB
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x000129DD File Offset: 0x00010BDD
		public long VersionNumber
		{
			get
			{
				return (long)this[SqlDBVersion.VersionNumberProp];
			}
			set
			{
				this[SqlDBVersion.VersionNumberProp] = value;
			}
		}

		// Token: 0x04000370 RID: 880
		public static readonly HygienePropertyDefinition VersionIdProp = new HygienePropertyDefinition("VersionId", typeof(Guid));

		// Token: 0x04000371 RID: 881
		public static readonly HygienePropertyDefinition VersionStringProp = new HygienePropertyDefinition("VersionString", typeof(string));

		// Token: 0x04000372 RID: 882
		public static readonly HygienePropertyDefinition VersionNumberProp = new HygienePropertyDefinition("VersionNumber", typeof(long), long.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
