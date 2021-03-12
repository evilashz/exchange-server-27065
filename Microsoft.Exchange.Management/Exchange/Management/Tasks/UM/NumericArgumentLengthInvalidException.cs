using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011DD RID: 4573
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NumericArgumentLengthInvalidException : LocalizedException
	{
		// Token: 0x0600B92E RID: 47406 RVA: 0x002A58A5 File Offset: 0x002A3AA5
		public NumericArgumentLengthInvalidException(string value, string argument, int maxSize) : base(Strings.ExceptionNumericArgumentLengthInvalid(value, argument, maxSize))
		{
			this.value = value;
			this.argument = argument;
			this.maxSize = maxSize;
		}

		// Token: 0x0600B92F RID: 47407 RVA: 0x002A58CA File Offset: 0x002A3ACA
		public NumericArgumentLengthInvalidException(string value, string argument, int maxSize, Exception innerException) : base(Strings.ExceptionNumericArgumentLengthInvalid(value, argument, maxSize), innerException)
		{
			this.value = value;
			this.argument = argument;
			this.maxSize = maxSize;
		}

		// Token: 0x0600B930 RID: 47408 RVA: 0x002A58F4 File Offset: 0x002A3AF4
		protected NumericArgumentLengthInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.value = (string)info.GetValue("value", typeof(string));
			this.argument = (string)info.GetValue("argument", typeof(string));
			this.maxSize = (int)info.GetValue("maxSize", typeof(int));
		}

		// Token: 0x0600B931 RID: 47409 RVA: 0x002A5969 File Offset: 0x002A3B69
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("value", this.value);
			info.AddValue("argument", this.argument);
			info.AddValue("maxSize", this.maxSize);
		}

		// Token: 0x17003A37 RID: 14903
		// (get) Token: 0x0600B932 RID: 47410 RVA: 0x002A59A6 File Offset: 0x002A3BA6
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17003A38 RID: 14904
		// (get) Token: 0x0600B933 RID: 47411 RVA: 0x002A59AE File Offset: 0x002A3BAE
		public string Argument
		{
			get
			{
				return this.argument;
			}
		}

		// Token: 0x17003A39 RID: 14905
		// (get) Token: 0x0600B934 RID: 47412 RVA: 0x002A59B6 File Offset: 0x002A3BB6
		public int MaxSize
		{
			get
			{
				return this.maxSize;
			}
		}

		// Token: 0x04006452 RID: 25682
		private readonly string value;

		// Token: 0x04006453 RID: 25683
		private readonly string argument;

		// Token: 0x04006454 RID: 25684
		private readonly int maxSize;
	}
}
