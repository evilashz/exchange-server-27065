using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EC3 RID: 3779
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MultipleSamePublicFolderMRSJobInstancesNotAllowedException : ManagementObjectAlreadyExistsException
	{
		// Token: 0x0600A8A8 RID: 43176 RVA: 0x0028A5D5 File Offset: 0x002887D5
		public MultipleSamePublicFolderMRSJobInstancesNotAllowedException(string requestType) : base(Strings.ErrorSamePublicFolderMRSJobInstancesNotAllowed(requestType))
		{
			this.requestType = requestType;
		}

		// Token: 0x0600A8A9 RID: 43177 RVA: 0x0028A5EA File Offset: 0x002887EA
		public MultipleSamePublicFolderMRSJobInstancesNotAllowedException(string requestType, Exception innerException) : base(Strings.ErrorSamePublicFolderMRSJobInstancesNotAllowed(requestType), innerException)
		{
			this.requestType = requestType;
		}

		// Token: 0x0600A8AA RID: 43178 RVA: 0x0028A600 File Offset: 0x00288800
		protected MultipleSamePublicFolderMRSJobInstancesNotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.requestType = (string)info.GetValue("requestType", typeof(string));
		}

		// Token: 0x0600A8AB RID: 43179 RVA: 0x0028A62A File Offset: 0x0028882A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("requestType", this.requestType);
		}

		// Token: 0x170036B9 RID: 14009
		// (get) Token: 0x0600A8AC RID: 43180 RVA: 0x0028A645 File Offset: 0x00288845
		public string RequestType
		{
			get
			{
				return this.requestType;
			}
		}

		// Token: 0x0400601F RID: 24607
		private readonly string requestType;
	}
}
