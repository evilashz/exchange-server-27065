using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000787 RID: 1927
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class UnsupportedPropertyInConversationLoadException : StoragePermanentException
	{
		// Token: 0x060048F0 RID: 18672 RVA: 0x00131A0B File Offset: 0x0012FC0B
		public UnsupportedPropertyInConversationLoadException(LocalizedString message, PropertyDefinition unsupportedProperty) : base(message)
		{
			this.unsupportedProperty = unsupportedProperty;
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x00131A1B File Offset: 0x0012FC1B
		protected UnsupportedPropertyInConversationLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.unsupportedProperty = (PropertyDefinition)info.GetValue("unsupportedProperty", typeof(PropertyDefinition));
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x00131A45 File Offset: 0x0012FC45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("unsupportedProperty", this.unsupportedProperty);
		}

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x060048F3 RID: 18675 RVA: 0x00131A60 File Offset: 0x0012FC60
		public PropertyDefinition UnsupportedProperty
		{
			get
			{
				return this.unsupportedProperty;
			}
		}

		// Token: 0x04002775 RID: 10101
		private const string UnsupportedPropertyLabel = "unsupportedProperty";

		// Token: 0x04002776 RID: 10102
		private PropertyDefinition unsupportedProperty;
	}
}
