using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Data.Services.Common;

namespace Microsoft.WindowsAzure.ActiveDirectoryV122
{
	// Token: 0x020005C2 RID: 1474
	[DataServiceKey("objectId")]
	public class ExtensionProperty : DirectoryObject
	{
		// Token: 0x060016D8 RID: 5848 RVA: 0x0002E4CC File Offset: 0x0002C6CC
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public static ExtensionProperty CreateExtensionProperty(string objectId, Collection<string> targetObjects)
		{
			ExtensionProperty extensionProperty = new ExtensionProperty();
			extensionProperty.objectId = objectId;
			if (targetObjects == null)
			{
				throw new ArgumentNullException("targetObjects");
			}
			extensionProperty.targetObjects = targetObjects;
			return extensionProperty;
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x0002E4FC File Offset: 0x0002C6FC
		// (set) Token: 0x060016DA RID: 5850 RVA: 0x0002E504 File Offset: 0x0002C704
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x0002E50D File Offset: 0x0002C70D
		// (set) Token: 0x060016DC RID: 5852 RVA: 0x0002E515 File Offset: 0x0002C715
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public string dataType
		{
			get
			{
				return this._dataType;
			}
			set
			{
				this._dataType = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x0002E51E File Offset: 0x0002C71E
		// (set) Token: 0x060016DE RID: 5854 RVA: 0x0002E526 File Offset: 0x0002C726
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		public Collection<string> targetObjects
		{
			get
			{
				return this._targetObjects;
			}
			set
			{
				this._targetObjects = value;
			}
		}

		// Token: 0x04001A5C RID: 6748
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _name;

		// Token: 0x04001A5D RID: 6749
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private string _dataType;

		// Token: 0x04001A5E RID: 6750
		[GeneratedCode("System.Data.Services.Design", "1.0.0")]
		private Collection<string> _targetObjects = new Collection<string>();
	}
}
