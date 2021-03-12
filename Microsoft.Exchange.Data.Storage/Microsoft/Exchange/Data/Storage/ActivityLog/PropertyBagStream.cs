using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x0200034A RID: 842
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class PropertyBagStream : MemoryStream
	{
		// Token: 0x0600252B RID: 9515 RVA: 0x00095BF8 File Offset: 0x00093DF8
		internal PropertyBagStream(MemoryPropertyBag propertyBag, PropertyDefinition propDef, PropertyType propertyType, int sizeEstimate) : base(sizeEstimate)
		{
			Util.ThrowOnNullArgument(propertyBag, "propertyBag");
			Util.ThrowOnNullArgument(propDef, "propDef");
			if (propertyType != PropertyType.Unicode && propertyType != PropertyType.Binary)
			{
				throw new NotSupportedException(string.Format("PropertyBagStream only supports Unicode and Binary streams, actual type: {0}.", propertyType));
			}
			this.propertyBag = propertyBag;
			this.propDef = propDef;
			this.propertyType = propertyType;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x00095C5C File Offset: 0x00093E5C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.propertyBag != null)
			{
				if (this.propertyType == PropertyType.Unicode)
				{
					this.propertyBag[this.propDef] = Encoding.Unicode.GetString(this.ToArray(), 0, (int)this.Length);
				}
				else
				{
					this.propertyBag[this.propDef] = this.ToArray();
				}
				this.propertyBag = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400169D RID: 5789
		private readonly PropertyDefinition propDef;

		// Token: 0x0400169E RID: 5790
		private readonly PropertyType propertyType;

		// Token: 0x0400169F RID: 5791
		private MemoryPropertyBag propertyBag;
	}
}
