using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000768 RID: 1896
	[Serializable]
	public class PropertyValidationException : CorruptDataException
	{
		// Token: 0x06004893 RID: 18579 RVA: 0x00131381 File Offset: 0x0012F581
		internal PropertyValidationException(string firstPropertyError, PropertyDefinition firstFailedProperty, PropertyValidationError[] errors) : base(ServerStrings.ExPropertyValidationFailed(firstPropertyError, (firstFailedProperty == null) ? null : firstFailedProperty.ToString()))
		{
			this.propertyValidationErrors = errors;
		}

		// Token: 0x06004894 RID: 18580 RVA: 0x001313A2 File Offset: 0x0012F5A2
		protected PropertyValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.propertyValidationErrors = (PropertyValidationError[])info.GetValue("propertyValidationErrors", typeof(PropertyValidationError[]));
		}

		// Token: 0x06004895 RID: 18581 RVA: 0x001313CC File Offset: 0x0012F5CC
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("propertyValidationErrors", this.propertyValidationErrors);
		}

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x06004896 RID: 18582 RVA: 0x001313E7 File Offset: 0x0012F5E7
		public PropertyValidationError[] PropertyValidationErrors
		{
			get
			{
				return this.propertyValidationErrors;
			}
		}

		// Token: 0x0400275E RID: 10078
		private const string PropertyValidationErrorsLabel = "propertyValidationErrors";

		// Token: 0x0400275F RID: 10079
		private readonly PropertyValidationError[] propertyValidationErrors;
	}
}
