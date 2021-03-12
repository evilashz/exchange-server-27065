using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000052 RID: 82
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CorruptLogDetectedException : TransientException
	{
		// Token: 0x0600028E RID: 654 RVA: 0x00009602 File Offset: 0x00007802
		public CorruptLogDetectedException(string filename, string errorText) : base(Strings.CorruptLogDetectedError(filename, errorText))
		{
			this.filename = filename;
			this.errorText = errorText;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000961F File Offset: 0x0000781F
		public CorruptLogDetectedException(string filename, string errorText, Exception innerException) : base(Strings.CorruptLogDetectedError(filename, errorText), innerException)
		{
			this.filename = filename;
			this.errorText = errorText;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009640 File Offset: 0x00007840
		protected CorruptLogDetectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filename = (string)info.GetValue("filename", typeof(string));
			this.errorText = (string)info.GetValue("errorText", typeof(string));
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009695 File Offset: 0x00007895
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filename", this.filename);
			info.AddValue("errorText", this.errorText);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000292 RID: 658 RVA: 0x000096C1 File Offset: 0x000078C1
		public string Filename
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000096C9 File Offset: 0x000078C9
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x0400016C RID: 364
		private readonly string filename;

		// Token: 0x0400016D RID: 365
		private readonly string errorText;
	}
}
