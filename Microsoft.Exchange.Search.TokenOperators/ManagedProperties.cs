using System;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Exchange.Search.OperatorSchema;

namespace Microsoft.Exchange.Search.TokenOperators
{
	// Token: 0x0200001B RID: 27
	public static class ManagedProperties
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00005668 File Offset: 0x00003868
		public static void SetAsPartiallyProcessed(IRecord record)
		{
			if (record == null)
			{
				throw new ArgumentNullException("record");
			}
			IUpdateableBucketField updateableBucketField = record["properties"] as IUpdateableBucketField;
			if (updateableBucketField[FastIndexSystemSchema.IsPartiallyProcessed.Name] == null)
			{
				updateableBucketField.AddField(FastIndexSystemSchema.IsPartiallyProcessed.Name, StandardFields.GetStandardBooleanField(new bool?(true)), BuiltInTypes.BooleanType);
				return;
			}
			IUpdateableBooleanField updateableBooleanField = updateableBucketField[FastIndexSystemSchema.IsPartiallyProcessed.Name] as IUpdateableBooleanField;
			if (updateableBooleanField == null)
			{
				throw new EvaluationException(string.Format("Abort processing (Cannot report partial processing status since the property \"{0}\" exists and is not Boolean).", FastIndexSystemSchema.IsPartiallyProcessed.Name));
			}
			updateableBooleanField.BooleanValue = true;
		}

		// Token: 0x04000069 RID: 105
		private const string PropertiesBucketFieldName = "properties";
	}
}
