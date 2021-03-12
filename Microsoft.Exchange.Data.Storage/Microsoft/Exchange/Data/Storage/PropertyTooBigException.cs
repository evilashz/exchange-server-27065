using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000767 RID: 1895
	[Serializable]
	public class PropertyTooBigException : CorruptDataException
	{
		// Token: 0x0600488F RID: 18575 RVA: 0x0013131F File Offset: 0x0012F51F
		public PropertyTooBigException(PropertyDefinition propertyDefinition) : base(ServerStrings.ExPropertyRequiresStreaming(propertyDefinition))
		{
			this.propertyDefinition = propertyDefinition;
		}

		// Token: 0x06004890 RID: 18576 RVA: 0x00131334 File Offset: 0x0012F534
		protected PropertyTooBigException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyDefinition = (PropertyDefinition)info.GetValue("propertyDefinition", typeof(PropertyDefinition));
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x0013135E File Offset: 0x0012F55E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyDefinition", this.propertyDefinition);
		}

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06004892 RID: 18578 RVA: 0x00131379 File Offset: 0x0012F579
		public PropertyDefinition PropertyDefinition
		{
			get
			{
				return this.propertyDefinition;
			}
		}

		// Token: 0x0400275C RID: 10076
		private const string PropertyDefinitionLabel = "propertyDefinition";

		// Token: 0x0400275D RID: 10077
		private PropertyDefinition propertyDefinition;
	}
}
