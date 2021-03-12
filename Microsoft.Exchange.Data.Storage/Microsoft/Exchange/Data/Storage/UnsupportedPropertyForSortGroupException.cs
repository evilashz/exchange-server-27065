using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000786 RID: 1926
	[Serializable]
	public class UnsupportedPropertyForSortGroupException : StoragePermanentException
	{
		// Token: 0x060048EC RID: 18668 RVA: 0x001319AE File Offset: 0x0012FBAE
		public UnsupportedPropertyForSortGroupException(LocalizedString message, PropertyDefinition unsupportedProperty) : base(message)
		{
			this.unsupportedProperty = unsupportedProperty;
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x001319BE File Offset: 0x0012FBBE
		protected UnsupportedPropertyForSortGroupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.unsupportedProperty = (PropertyDefinition)info.GetValue("unsupportedProperty", typeof(PropertyDefinition));
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x001319E8 File Offset: 0x0012FBE8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("unsupportedProperty", this.unsupportedProperty);
		}

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x060048EF RID: 18671 RVA: 0x00131A03 File Offset: 0x0012FC03
		public PropertyDefinition UnsupportedProperty
		{
			get
			{
				return this.unsupportedProperty;
			}
		}

		// Token: 0x04002773 RID: 10099
		private const string UnsupportedPropertyLabel = "unsupportedProperty";

		// Token: 0x04002774 RID: 10100
		private PropertyDefinition unsupportedProperty;
	}
}
