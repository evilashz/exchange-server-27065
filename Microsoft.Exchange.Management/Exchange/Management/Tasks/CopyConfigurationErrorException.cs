using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EE4 RID: 3812
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CopyConfigurationErrorException : LocalizedException
	{
		// Token: 0x0600A950 RID: 43344 RVA: 0x0028B695 File Offset: 0x00289895
		public CopyConfigurationErrorException(string exception) : base(Strings.CopyConfigurationErrorException(exception))
		{
			this.exception = exception;
		}

		// Token: 0x0600A951 RID: 43345 RVA: 0x0028B6AA File Offset: 0x002898AA
		public CopyConfigurationErrorException(string exception, Exception innerException) : base(Strings.CopyConfigurationErrorException(exception), innerException)
		{
			this.exception = exception;
		}

		// Token: 0x0600A952 RID: 43346 RVA: 0x0028B6C0 File Offset: 0x002898C0
		protected CopyConfigurationErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.exception = (string)info.GetValue("exception", typeof(string));
		}

		// Token: 0x0600A953 RID: 43347 RVA: 0x0028B6EA File Offset: 0x002898EA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("exception", this.exception);
		}

		// Token: 0x170036DD RID: 14045
		// (get) Token: 0x0600A954 RID: 43348 RVA: 0x0028B705 File Offset: 0x00289905
		public string Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04006043 RID: 24643
		private readonly string exception;
	}
}
