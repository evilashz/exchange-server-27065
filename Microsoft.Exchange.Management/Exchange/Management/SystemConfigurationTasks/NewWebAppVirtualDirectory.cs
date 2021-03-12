using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C20 RID: 3104
	public abstract class NewWebAppVirtualDirectory<T> : NewExchangeVirtualDirectory<T> where T : ExchangeWebAppVirtualDirectory, new()
	{
		// Token: 0x170023EC RID: 9196
		// (get) Token: 0x060074FE RID: 29950 RVA: 0x001DDFDC File Offset: 0x001DC1DC
		internal override MetabasePropertyTypes.AppPoolIdentityType AppPoolIdentityType
		{
			get
			{
				return MetabasePropertyTypes.AppPoolIdentityType.LocalSystem;
			}
		}

		// Token: 0x170023ED RID: 9197
		// (get) Token: 0x060074FF RID: 29951 RVA: 0x001DDFE0 File Offset: 0x001DC1E0
		protected string AppRootValue
		{
			get
			{
				return base.RetrieveVDirAppRootValue(base.GetAbsolutePath(IisUtility.AbsolutePathType.WebSiteRoot), base.Name);
			}
		}

		// Token: 0x06007500 RID: 29952 RVA: 0x001DE004 File Offset: 0x001DC204
		protected override void InternalValidate()
		{
			base.InternalValidate();
			T dataObject = this.DataObject;
			dataObject.WebSite = base.WebSiteName;
		}

		// Token: 0x06007501 RID: 29953 RVA: 0x001DE034 File Offset: 0x001DC234
		protected override void WriteResultMetabaseFixup(ExchangeVirtualDirectory targetDataObject)
		{
			ExchangeWebAppVirtualDirectory target = (ExchangeWebAppVirtualDirectory)targetDataObject;
			WebAppVirtualDirectoryHelper.CopyMetabaseProperties(target, this.DataObject);
		}

		// Token: 0x170023EE RID: 9198
		// (get) Token: 0x06007502 RID: 29954 RVA: 0x001DE059 File Offset: 0x001DC259
		internal new MultiValuedProperty<AuthenticationMethod> InternalAuthenticationMethods
		{
			get
			{
				return base.InternalAuthenticationMethods;
			}
		}

		// Token: 0x06007503 RID: 29955 RVA: 0x001DE064 File Offset: 0x001DC264
		internal void AddBinVDir(ListDictionary childVDirs)
		{
			this.AddChildVDir(childVDirs, "bin", new MetabaseProperty[]
			{
				new MetabaseProperty("AccessFlags", MetabasePropertyTypes.AccessFlags.NoAccess)
			});
		}

		// Token: 0x06007504 RID: 29956 RVA: 0x001DE098 File Offset: 0x001DC298
		internal void AddChildVDir(ListDictionary childVDirs, string childFolder, IList<MetabaseProperty> vdirProperties)
		{
			TaskLogger.LogEnter(new object[]
			{
				childFolder
			});
			string path = System.IO.Path.Combine(base.Path, childFolder);
			if (!Directory.Exists(path))
			{
				this.WriteWarning(Strings.ErrorChildFolderNotExistsOnServer(path, base.OwningServer.Name));
			}
			childVDirs.Add(childFolder, vdirProperties);
			TaskLogger.LogExit();
		}
	}
}
