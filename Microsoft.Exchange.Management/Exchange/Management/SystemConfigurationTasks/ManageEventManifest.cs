using System;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008AD RID: 2221
	internal static class ManageEventManifest
	{
		// Token: 0x06004E68 RID: 20072 RVA: 0x0014538C File Offset: 0x0014358C
		private static void DoManifestAction(string manifestName, string actionString)
		{
			string path = Path.Combine(Environment.GetEnvironmentVariable("windir"), "system32");
			string executableFilename = Path.Combine(path, "wevtutil.exe");
			string arguments = actionString + " \"" + manifestName + "\"";
			string text;
			string errors;
			int num = ProcessRunner.Run(executableFilename, arguments, -1, null, out text, out errors);
			if (num != 0)
			{
				throw new InvalidOperationException(Strings.EventManifestActionFailed(manifestName, actionString, num, errors));
			}
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x001453F8 File Offset: 0x001435F8
		private static void DoWevtutilAction(string cmdlineArguments)
		{
			string path = Path.Combine(Environment.GetEnvironmentVariable("windir"), "system32");
			string executableFilename = Path.Combine(path, "wevtutil.exe");
			string text;
			string errors;
			int num = ProcessRunner.Run(executableFilename, cmdlineArguments, -1, null, out text, out errors);
			if (num != 0)
			{
				throw new InvalidOperationException(Strings.EventOtherActionFailed(cmdlineArguments, num, errors));
			}
		}

		// Token: 0x06004E6A RID: 20074 RVA: 0x0014544D File Offset: 0x0014364D
		internal static void Install(string manifestName)
		{
			ManageEventManifest.DoManifestAction(manifestName, "install-manifest");
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x0014545A File Offset: 0x0014365A
		internal static void Uninstall(string manifestName)
		{
			ManageEventManifest.DoManifestAction(manifestName, "uninstall-manifest");
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x00145467 File Offset: 0x00143667
		internal static void SetChannelAttribute(string verb, string channelName, string arguments)
		{
			ManageEventManifest.DoWevtutilAction(string.Format("{0} {1} {2}", verb, channelName, arguments));
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x0014547C File Offset: 0x0014367C
		internal static bool UpdateMessageDllPath(string manifestName, string msgDll, string providerName)
		{
			bool flag = false;
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(manifestName);
			using (XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("provider"))
			{
				foreach (object obj in elementsByTagName)
				{
					XmlNode xmlNode = (XmlNode)obj;
					XmlAttribute xmlAttribute = (XmlAttribute)xmlNode.Attributes.GetNamedItem("name");
					if (string.Equals(xmlAttribute.Value, providerName, StringComparison.OrdinalIgnoreCase))
					{
						xmlAttribute = (XmlAttribute)xmlNode.Attributes.GetNamedItem("resourceFileName");
						xmlAttribute.Value = msgDll;
						xmlAttribute = (XmlAttribute)xmlNode.Attributes.GetNamedItem("messageFileName");
						xmlAttribute.Value = msgDll;
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				xmlDocument.Save(manifestName);
			}
			return flag;
		}
	}
}
