using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000FDB RID: 4059
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ClassificationRuleCollectionSchemaValidationException : ClassificationRuleCollectionValidationException
	{
		// Token: 0x0600AE13 RID: 44563 RVA: 0x00292866 File Offset: 0x00290A66
		public ClassificationRuleCollectionSchemaValidationException(string schemaError, int lineNumber, int linePosition) : base(Strings.ClassificationRuleCollectionSchemaNonConformance(schemaError, lineNumber, linePosition))
		{
			this.schemaError = schemaError;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x0600AE14 RID: 44564 RVA: 0x0029288B File Offset: 0x00290A8B
		public ClassificationRuleCollectionSchemaValidationException(string schemaError, int lineNumber, int linePosition, Exception innerException) : base(Strings.ClassificationRuleCollectionSchemaNonConformance(schemaError, lineNumber, linePosition), innerException)
		{
			this.schemaError = schemaError;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x0600AE15 RID: 44565 RVA: 0x002928B4 File Offset: 0x00290AB4
		protected ClassificationRuleCollectionSchemaValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.schemaError = (string)info.GetValue("schemaError", typeof(string));
			this.lineNumber = (int)info.GetValue("lineNumber", typeof(int));
			this.linePosition = (int)info.GetValue("linePosition", typeof(int));
		}

		// Token: 0x0600AE16 RID: 44566 RVA: 0x00292929 File Offset: 0x00290B29
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("schemaError", this.schemaError);
			info.AddValue("lineNumber", this.lineNumber);
			info.AddValue("linePosition", this.linePosition);
		}

		// Token: 0x170037C4 RID: 14276
		// (get) Token: 0x0600AE17 RID: 44567 RVA: 0x00292966 File Offset: 0x00290B66
		public string SchemaError
		{
			get
			{
				return this.schemaError;
			}
		}

		// Token: 0x170037C5 RID: 14277
		// (get) Token: 0x0600AE18 RID: 44568 RVA: 0x0029296E File Offset: 0x00290B6E
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x170037C6 RID: 14278
		// (get) Token: 0x0600AE19 RID: 44569 RVA: 0x00292976 File Offset: 0x00290B76
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x0400612A RID: 24874
		private readonly string schemaError;

		// Token: 0x0400612B RID: 24875
		private readonly int lineNumber;

		// Token: 0x0400612C RID: 24876
		private readonly int linePosition;
	}
}
