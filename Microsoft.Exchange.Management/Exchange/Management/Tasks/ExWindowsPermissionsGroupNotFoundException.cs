using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E08 RID: 3592
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExWindowsPermissionsGroupNotFoundException : LocalizedException
	{
		// Token: 0x0600A523 RID: 42275 RVA: 0x00285783 File Offset: 0x00283983
		public ExWindowsPermissionsGroupNotFoundException(Guid guid) : base(Strings.ExWindowsPermissionsGroupNotFoundException(guid))
		{
			this.guid = guid;
		}

		// Token: 0x0600A524 RID: 42276 RVA: 0x00285798 File Offset: 0x00283998
		public ExWindowsPermissionsGroupNotFoundException(Guid guid, Exception innerException) : base(Strings.ExWindowsPermissionsGroupNotFoundException(guid), innerException)
		{
			this.guid = guid;
		}

		// Token: 0x0600A525 RID: 42277 RVA: 0x002857AE File Offset: 0x002839AE
		protected ExWindowsPermissionsGroupNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.guid = (Guid)info.GetValue("guid", typeof(Guid));
		}

		// Token: 0x0600A526 RID: 42278 RVA: 0x002857D8 File Offset: 0x002839D8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("guid", this.guid);
		}

		// Token: 0x17003620 RID: 13856
		// (get) Token: 0x0600A527 RID: 42279 RVA: 0x002857F8 File Offset: 0x002839F8
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		// Token: 0x04005F86 RID: 24454
		private readonly Guid guid;
	}
}
