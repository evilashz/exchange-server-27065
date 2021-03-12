using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	public class ObjectValidationError : ValidationError
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00005361 File Offset: 0x00003561
		public ObjectValidationError(LocalizedString description, ObjectId objectId, string dataSourceName) : base(description)
		{
			this.objectId = objectId;
			this.dataSourceName = dataSourceName;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005378 File Offset: 0x00003578
		public ObjectId ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005380 File Offset: 0x00003580
		public string DataSourceName
		{
			get
			{
				return this.dataSourceName;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005388 File Offset: 0x00003588
		public bool Equals(ObjectValidationError other)
		{
			return other != null && string.Equals(this.DataSourceName, other.DataSourceName, StringComparison.OrdinalIgnoreCase) && object.Equals(this.ObjectId, other.ObjectId) && base.Equals(other);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000053BF File Offset: 0x000035BF
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ObjectValidationError);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000053D0 File Offset: 0x000035D0
		public override int GetHashCode()
		{
			if (this.hashCode == 0)
			{
				this.hashCode = (base.GetHashCode() ^ (this.DataSourceName ?? string.Empty).ToLowerInvariant().GetHashCode() ^ ((this.ObjectId == null) ? 0 : this.ObjectId.GetHashCode()));
			}
			return this.hashCode;
		}

		// Token: 0x04000058 RID: 88
		private ObjectId objectId;

		// Token: 0x04000059 RID: 89
		private string dataSourceName;

		// Token: 0x0400005A RID: 90
		private int hashCode;
	}
}
