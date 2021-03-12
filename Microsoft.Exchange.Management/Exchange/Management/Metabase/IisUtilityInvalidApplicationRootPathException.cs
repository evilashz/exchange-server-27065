using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FAB RID: 4011
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IisUtilityInvalidApplicationRootPathException : LocalizedException
	{
		// Token: 0x0600AD24 RID: 44324 RVA: 0x002911AA File Offset: 0x0028F3AA
		public IisUtilityInvalidApplicationRootPathException(string applicationRootPath) : base(Strings.IisUtilityInvalidApplicationRootPathException(applicationRootPath))
		{
			this.applicationRootPath = applicationRootPath;
		}

		// Token: 0x0600AD25 RID: 44325 RVA: 0x002911BF File Offset: 0x0028F3BF
		public IisUtilityInvalidApplicationRootPathException(string applicationRootPath, Exception innerException) : base(Strings.IisUtilityInvalidApplicationRootPathException(applicationRootPath), innerException)
		{
			this.applicationRootPath = applicationRootPath;
		}

		// Token: 0x0600AD26 RID: 44326 RVA: 0x002911D5 File Offset: 0x0028F3D5
		protected IisUtilityInvalidApplicationRootPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.applicationRootPath = (string)info.GetValue("applicationRootPath", typeof(string));
		}

		// Token: 0x0600AD27 RID: 44327 RVA: 0x002911FF File Offset: 0x0028F3FF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("applicationRootPath", this.applicationRootPath);
		}

		// Token: 0x17003795 RID: 14229
		// (get) Token: 0x0600AD28 RID: 44328 RVA: 0x0029121A File Offset: 0x0028F41A
		public string ApplicationRootPath
		{
			get
			{
				return this.applicationRootPath;
			}
		}

		// Token: 0x040060FB RID: 24827
		private readonly string applicationRootPath;
	}
}
