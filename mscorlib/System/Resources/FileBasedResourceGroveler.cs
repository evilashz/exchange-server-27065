using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Resources
{
	// Token: 0x0200035F RID: 863
	internal class FileBasedResourceGroveler : IResourceGroveler
	{
		// Token: 0x06002BC5 RID: 11205 RVA: 0x000A48E6 File Offset: 0x000A2AE6
		public FileBasedResourceGroveler(ResourceManager.ResourceManagerMediator mediator)
		{
			this._mediator = mediator;
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000A48F8 File Offset: 0x000A2AF8
		[SecuritySafeCritical]
		public ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark)
		{
			ResourceSet resourceSet = null;
			ResourceSet result;
			try
			{
				new FileIOPermission(PermissionState.Unrestricted).Assert();
				string resourceFileName = this._mediator.GetResourceFileName(culture);
				string text = this.FindResourceFile(culture, resourceFileName);
				if (text == null)
				{
					if (tryParents && culture.HasInvariantCultureName)
					{
						throw new MissingManifestResourceException(string.Concat(new string[]
						{
							Environment.GetResourceString("MissingManifestResource_NoNeutralDisk"),
							Environment.NewLine,
							"baseName: ",
							this._mediator.BaseNameField,
							"  locationInfo: ",
							(this._mediator.LocationInfo == null) ? "<null>" : this._mediator.LocationInfo.FullName,
							"  fileName: ",
							this._mediator.GetResourceFileName(culture)
						}));
					}
				}
				else
				{
					resourceSet = this.CreateResourceSet(text);
				}
				result = resourceSet;
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000A49EC File Offset: 0x000A2BEC
		public bool HasNeutralResources(CultureInfo culture, string defaultResName)
		{
			string text = this.FindResourceFile(culture, defaultResName);
			if (text == null || !File.Exists(text))
			{
				string text2 = this._mediator.ModuleDir;
				if (text != null)
				{
					text2 = Path.GetDirectoryName(text);
				}
				return false;
			}
			return true;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000A4A28 File Offset: 0x000A2C28
		private string FindResourceFile(CultureInfo culture, string fileName)
		{
			if (this._mediator.ModuleDir != null)
			{
				string text = Path.Combine(this._mediator.ModuleDir, fileName);
				if (File.Exists(text))
				{
					return text;
				}
			}
			if (File.Exists(fileName))
			{
				return fileName;
			}
			return null;
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000A4A6C File Offset: 0x000A2C6C
		[SecurityCritical]
		private ResourceSet CreateResourceSet(string file)
		{
			if (this._mediator.UserResourceSet == null)
			{
				return new RuntimeResourceSet(file);
			}
			object[] args = new object[]
			{
				file
			};
			ResourceSet result;
			try
			{
				result = (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, args);
			}
			catch (MissingMethodException innerException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResMgrBadResSet_Type", new object[]
				{
					this._mediator.UserResourceSet.AssemblyQualifiedName
				}), innerException);
			}
			return result;
		}

		// Token: 0x04001170 RID: 4464
		private ResourceManager.ResourceManagerMediator _mediator;
	}
}
