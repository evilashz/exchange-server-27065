using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F2 RID: 498
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ExpressionException : LocalizedException
	{
		// Token: 0x06001082 RID: 4226 RVA: 0x00038E91 File Offset: 0x00037091
		public ExpressionException(string error) : base(Strings.ExpressionException(error))
		{
			this.error = error;
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x00038EA6 File Offset: 0x000370A6
		public ExpressionException(string error, Exception innerException) : base(Strings.ExpressionException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x00038EBC File Offset: 0x000370BC
		protected ExpressionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x00038EE6 File Offset: 0x000370E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00038F01 File Offset: 0x00037101
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04000878 RID: 2168
		private readonly string error;
	}
}
