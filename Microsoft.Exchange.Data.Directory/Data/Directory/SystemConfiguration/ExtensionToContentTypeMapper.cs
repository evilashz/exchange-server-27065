using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200046A RID: 1130
	internal class ExtensionToContentTypeMapper
	{
		// Token: 0x0600324D RID: 12877 RVA: 0x000CB8EF File Offset: 0x000C9AEF
		private ExtensionToContentTypeMapper()
		{
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x0600324E RID: 12878 RVA: 0x000CB910 File Offset: 0x000C9B10
		public static ExtensionToContentTypeMapper Instance
		{
			get
			{
				if (ExtensionToContentTypeMapper.instance == null)
				{
					lock (ExtensionToContentTypeMapper.lockContentType)
					{
						ExtensionToContentTypeMapper.instance = ExtensionToContentTypeMapper.Create();
					}
				}
				return ExtensionToContentTypeMapper.instance;
			}
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000CB960 File Offset: 0x000C9B60
		public string GetContentTypeByExtension(string extension)
		{
			string result = null;
			if (this.contentTypeDictionary.TryGetValue(extension.ToLowerInvariant(), out result))
			{
				return result;
			}
			return "application/octet-stream";
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000CB98C File Offset: 0x000C9B8C
		public string GetExtensionByContentType(string contentType)
		{
			string result = null;
			if (this.extensionDictionary.TryGetValue(contentType.ToLowerInvariant(), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000CB9B4 File Offset: 0x000C9BB4
		private static ExtensionToContentTypeMapper Create()
		{
			if (ExtensionToContentTypeMapper.instance != null)
			{
				return ExtensionToContentTypeMapper.instance;
			}
			ExtensionToContentTypeMapper extensionToContentTypeMapper = new ExtensionToContentTypeMapper();
			extensionToContentTypeMapper.BuildContentTypesFromAD();
			extensionToContentTypeMapper.BuildContentTypesFromRegistry();
			return extensionToContentTypeMapper;
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000CB9E1 File Offset: 0x000C9BE1
		private static void AddNewEntry(Dictionary<string, string> dict, string key, string value)
		{
			if (!dict.ContainsKey(key))
			{
				dict.Add(key, value);
			}
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000CB9F4 File Offset: 0x000C9BF4
		private void BuildContentTypesFromAD()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 133, "BuildContentTypesFromAD", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ExtensionToContentType.cs");
			Organization organization = null;
			try
			{
				organization = tenantOrTopologyConfigurationSession.GetOrgContainer();
				ExtensionToContentTypeMapper.organizationName = organization.Name.ToLowerInvariant();
			}
			catch (OrgContainerNotFoundException)
			{
				organization = null;
				ExtensionToContentTypeMapper.organizationName = string.Empty;
			}
			if (organization == null)
			{
				return;
			}
			foreach (string text in organization.MimeTypes)
			{
				int num = text.IndexOf(";");
				if (num >= 0)
				{
					string text2 = text.Substring(0, num).ToLowerInvariant();
					string text3 = text.Substring(num + 1).ToLowerInvariant();
					if (!(text2 == string.Empty) && !(text3 == string.Empty))
					{
						ExtensionToContentTypeMapper.AddNewEntry(this.contentTypeDictionary, text3, text2);
						ExtensionToContentTypeMapper.AddNewEntry(this.extensionDictionary, text2, text3);
					}
				}
			}
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000CBB04 File Offset: 0x000C9D04
		private void BuildContentTypesFromRegistry()
		{
			try
			{
				string[] subKeyNames = Registry.ClassesRoot.GetSubKeyNames();
				foreach (string text in subKeyNames)
				{
					if (text.StartsWith(".") && text.Length != 1)
					{
						string key = text.Substring(1).ToLowerInvariant();
						try
						{
							using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(text))
							{
								string value = registryKey.GetValue("Content Type") as string;
								if (!string.IsNullOrEmpty(value))
								{
									ExtensionToContentTypeMapper.AddNewEntry(this.contentTypeDictionary, key, value);
								}
							}
						}
						catch (IOException arg)
						{
							ExTraceGlobals.ContentTypeMappingTracer.TraceError<string, IOException>(0L, "ExtensionToContentTypeMapper::BuildContentTypeFromRegistry. I/O error opening the registry key {0}. Exception = {1}.", text, arg);
						}
						catch (SecurityException arg2)
						{
							ExTraceGlobals.ContentTypeMappingTracer.TraceError<string, SecurityException>(0L, "ExtensionToContentTypeMapper::BuildContentTypeFromRegistry. We are not allowed to open the registry key {0}. Exception = {1}.", text, arg2);
						}
					}
				}
			}
			catch (SecurityException ex)
			{
				ExTraceGlobals.ContentTypeMappingTracer.TraceError<SecurityException>(0L, "ExtensionToContentTypeMapper::BuildContentTypeFromRegistry. We are not allowed to open the registry keys. Exception = {0}.", ex);
				throw new RegistryContentTypeException(ex);
			}
		}

		// Token: 0x04002290 RID: 8848
		public const string DefaultContentType = "application/octet-stream";

		// Token: 0x04002291 RID: 8849
		private const string ContentTypeRegSubKey = "Content Type";

		// Token: 0x04002292 RID: 8850
		private const int MaxOrganizationCount = 1000;

		// Token: 0x04002293 RID: 8851
		private static string organizationName;

		// Token: 0x04002294 RID: 8852
		private readonly Dictionary<string, string> contentTypeDictionary = new Dictionary<string, string>();

		// Token: 0x04002295 RID: 8853
		private readonly Dictionary<string, string> extensionDictionary = new Dictionary<string, string>();

		// Token: 0x04002296 RID: 8854
		private static ExtensionToContentTypeMapper instance = null;

		// Token: 0x04002297 RID: 8855
		private static object lockContentType = new object();
	}
}
