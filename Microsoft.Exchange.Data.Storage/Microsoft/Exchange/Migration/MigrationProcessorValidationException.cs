using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000144 RID: 324
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationProcessorValidationException : MigrationPermanentException
	{
		// Token: 0x060015BE RID: 5566 RVA: 0x0006E60B File Offset: 0x0006C80B
		public MigrationProcessorValidationException(string processor, string jobname) : base(Strings.MigrationProcessorInvalidation(processor, jobname))
		{
			this.processor = processor;
			this.jobname = jobname;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0006E628 File Offset: 0x0006C828
		public MigrationProcessorValidationException(string processor, string jobname, Exception innerException) : base(Strings.MigrationProcessorInvalidation(processor, jobname), innerException)
		{
			this.processor = processor;
			this.jobname = jobname;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0006E648 File Offset: 0x0006C848
		protected MigrationProcessorValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.processor = (string)info.GetValue("processor", typeof(string));
			this.jobname = (string)info.GetValue("jobname", typeof(string));
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0006E69D File Offset: 0x0006C89D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("processor", this.processor);
			info.AddValue("jobname", this.jobname);
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x0006E6C9 File Offset: 0x0006C8C9
		public string Processor
		{
			get
			{
				return this.processor;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0006E6D1 File Offset: 0x0006C8D1
		public string Jobname
		{
			get
			{
				return this.jobname;
			}
		}

		// Token: 0x04000AD6 RID: 2774
		private readonly string processor;

		// Token: 0x04000AD7 RID: 2775
		private readonly string jobname;
	}
}
