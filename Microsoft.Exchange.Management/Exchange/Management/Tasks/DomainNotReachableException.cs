using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DF8 RID: 3576
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainNotReachableException : LocalizedException
	{
		// Token: 0x0600A4D4 RID: 42196 RVA: 0x00285008 File Offset: 0x00283208
		public DomainNotReachableException(string dom, string taskName) : base(Strings.DomainNotReachableException(dom, taskName))
		{
			this.dom = dom;
			this.taskName = taskName;
		}

		// Token: 0x0600A4D5 RID: 42197 RVA: 0x00285025 File Offset: 0x00283225
		public DomainNotReachableException(string dom, string taskName, Exception innerException) : base(Strings.DomainNotReachableException(dom, taskName), innerException)
		{
			this.dom = dom;
			this.taskName = taskName;
		}

		// Token: 0x0600A4D6 RID: 42198 RVA: 0x00285044 File Offset: 0x00283244
		protected DomainNotReachableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dom = (string)info.GetValue("dom", typeof(string));
			this.taskName = (string)info.GetValue("taskName", typeof(string));
		}

		// Token: 0x0600A4D7 RID: 42199 RVA: 0x00285099 File Offset: 0x00283299
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dom", this.dom);
			info.AddValue("taskName", this.taskName);
		}

		// Token: 0x17003611 RID: 13841
		// (get) Token: 0x0600A4D8 RID: 42200 RVA: 0x002850C5 File Offset: 0x002832C5
		public string Dom
		{
			get
			{
				return this.dom;
			}
		}

		// Token: 0x17003612 RID: 13842
		// (get) Token: 0x0600A4D9 RID: 42201 RVA: 0x002850CD File Offset: 0x002832CD
		public string TaskName
		{
			get
			{
				return this.taskName;
			}
		}

		// Token: 0x04005F77 RID: 24439
		private readonly string dom;

		// Token: 0x04005F78 RID: 24440
		private readonly string taskName;
	}
}
