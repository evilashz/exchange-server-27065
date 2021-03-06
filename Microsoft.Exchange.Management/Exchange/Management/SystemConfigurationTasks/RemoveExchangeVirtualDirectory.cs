using System;
using System.Collections;
using System.Management.Automation;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Metabase;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000BFF RID: 3071
	public abstract class RemoveExchangeVirtualDirectory<T> : RemoveVirtualDirectory<T> where T : ExchangeVirtualDirectory, new()
	{
		// Token: 0x060073D5 RID: 29653 RVA: 0x001D7CA8 File Offset: 0x001D5EA8
		protected override void InternalProcessRecord()
		{
			object[] array = new object[1];
			object[] array2 = array;
			int num = 0;
			T dataObject = base.DataObject;
			array2[num] = dataObject.Identity;
			TaskLogger.LogEnter(array);
			T dataObject2 = base.DataObject;
			byte major = dataObject2.ExchangeVersion.ExchangeBuild.Major;
			T dataObject3 = base.DataObject;
			if (major != dataObject3.MaximumSupportedExchangeObjectVersion.ExchangeBuild.Major)
			{
				T dataObject4 = base.DataObject;
				base.WriteError(new CannotModifyCrossVersionObjectException(dataObject4.Id.DistinguishedName), ErrorCategory.InvalidOperation, null);
				return;
			}
			base.InternalProcessRecord();
			try
			{
				this.PreDeleteFromMetabase();
				this.DeleteFromMetabase();
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2147023174)
				{
					T dataObject5 = base.DataObject;
					Exception exception = new IISNotReachableException(IisUtility.GetHostName(dataObject5.MetabasePath), ex.Message);
					ErrorCategory category = ErrorCategory.ReadError;
					T dataObject6 = base.DataObject;
					this.WriteError(exception, category, dataObject6.Identity, false);
					return;
				}
				T dataObject7 = base.DataObject;
				string name = dataObject7.Server.Name;
				T dataObject8 = base.DataObject;
				Exception exception2 = new InvalidOperationException(Strings.DeleteVirtualDirectoryFail(name, dataObject8.MetabasePath), ex);
				ErrorCategory category2 = ErrorCategory.InvalidOperation;
				T dataObject9 = base.DataObject;
				base.WriteError(exception2, category2, dataObject9.Identity);
				return;
			}
			catch (Exception innerException)
			{
				T dataObject10 = base.DataObject;
				string name2 = dataObject10.Server.Name;
				T dataObject11 = base.DataObject;
				Exception exception3 = new InvalidOperationException(Strings.DeleteVirtualDirectoryFail(name2, dataObject11.MetabasePath), innerException);
				ErrorCategory category3 = ErrorCategory.InvalidOperation;
				T dataObject12 = base.DataObject;
				base.WriteError(exception3, category3, dataObject12.Identity);
				return;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x170023A3 RID: 9123
		// (get) Token: 0x060073D6 RID: 29654 RVA: 0x001D7E8C File Offset: 0x001D608C
		protected virtual ICollection ChildVirtualDirectoryNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060073D7 RID: 29655 RVA: 0x001D7E8F File Offset: 0x001D608F
		protected virtual void PreDeleteFromMetabase()
		{
		}

		// Token: 0x060073D8 RID: 29656 RVA: 0x001D7E94 File Offset: 0x001D6094
		protected virtual void DeleteFromMetabase()
		{
			T dataObject = base.DataObject;
			int num = dataObject.MetabasePath.LastIndexOf('/');
			T dataObject2 = base.DataObject;
			string webSiteRoot = dataObject2.MetabasePath.Substring(0, num);
			T dataObject3 = base.DataObject;
			string virtualDirectoryName = dataObject3.MetabasePath.Substring(num + 1);
			DeleteVirtualDirectory.DeleteFromMetabase(webSiteRoot, virtualDirectoryName, this.ChildVirtualDirectoryNames);
		}
	}
}
