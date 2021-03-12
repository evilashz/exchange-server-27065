using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000006 RID: 6
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataValidationException : LocalizedException, IErrorContextException
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000231D File Offset: 0x0000051D
		public DataValidationException(ValidationError error) : base(error.Description)
		{
			this.error = error;
			this.propertyName = error.PropertyName;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000233E File Offset: 0x0000053E
		public DataValidationException(ValidationError error, Exception innerException) : base(error.Description, innerException)
		{
			this.error = error;
			this.propertyName = error.PropertyName;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002360 File Offset: 0x00000560
		protected DataValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000236A File Offset: 0x0000056A
		public ValidationError Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002372 File Offset: 0x00000572
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000237C File Offset: 0x0000057C
		public override string Message
		{
			get
			{
				if (this.context == null || this.context.ExecutionHost.Equals("Exchange Control Panel"))
				{
					return base.Message;
				}
				if (this.errorMessage == null)
				{
					this.errorMessage = base.Message + " " + DataStrings.PropertyName(this.propertyName);
				}
				return this.errorMessage;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023E3 File Offset: 0x000005E3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000023ED File Offset: 0x000005ED
		public void SetContext(IErrorExecutionContext context)
		{
			this.context = context;
		}

		// Token: 0x04000002 RID: 2
		private readonly ValidationError error;

		// Token: 0x04000003 RID: 3
		private readonly string propertyName;

		// Token: 0x04000004 RID: 4
		private IErrorExecutionContext context;

		// Token: 0x04000005 RID: 5
		private string errorMessage;
	}
}
