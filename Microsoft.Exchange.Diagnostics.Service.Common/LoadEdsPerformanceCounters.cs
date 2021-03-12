using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Diagnostics.Service.Common
{
	// Token: 0x0200000D RID: 13
	internal static class LoadEdsPerformanceCounters
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000045F0 File Offset: 0x000027F0
		internal static void RegisterCounters(string installationPath)
		{
			string filename = Path.Combine(installationPath, "Microsoft.Exchange.Diagnostics.Service.Common.EdsPerformanceCounters.xml");
			SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
			safeXmlDocument.Load(filename);
			string text = LoadEdsPerformanceCounters.ReadStringElement(safeXmlDocument, "Category/Name");
			Logger.LogInformationMessage("Found performance counter category {0}", new object[]
			{
				text
			});
			if (!LoadEdsPerformanceCounters.SafeExists(text))
			{
				Logger.LogInformationMessage("Registering EDS performance counters.", new object[0]);
				CounterCreationDataCollection counterCreationDataCollection = new CounterCreationDataCollection();
				XmlNodeList xmlNodeList = safeXmlDocument.SelectNodes("Category/Counters/Counter");
				using (xmlNodeList)
				{
					for (int i = 0; i < xmlNodeList.Count; i++)
					{
						string text2 = LoadEdsPerformanceCounters.ReadStringElement(xmlNodeList[i], "Name");
						PerformanceCounterType counterType = (PerformanceCounterType)Enum.Parse(typeof(PerformanceCounterType), LoadEdsPerformanceCounters.ReadStringElement(xmlNodeList[i], "Type"), true);
						counterCreationDataCollection.Add(new CounterCreationData(text2, string.Empty, counterType));
						Logger.LogInformationMessage("Loaded counter {0}", new object[]
						{
							text2
						});
					}
				}
				LoadEdsPerformanceCounters.SafeCreate(text, counterCreationDataCollection);
				return;
			}
			Logger.LogInformationMessage("Counters are already registered.", new object[0]);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00004728 File Offset: 0x00002928
		private static void LoadHelp()
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000472C File Offset: 0x0000292C
		private static string ReadStringElement(XmlNode node, string xpath)
		{
			XmlNodeList xmlNodeList = node.SelectNodes(xpath);
			string innerText;
			using (xmlNodeList)
			{
				if (xmlNodeList.Count != 1)
				{
					throw new ArgumentException("Invalid performance counter files");
				}
				innerText = xmlNodeList[0].InnerText;
			}
			return innerText;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00004784 File Offset: 0x00002984
		private static bool SafeExists(string categoryName)
		{
			try
			{
				return PerformanceCounterCategory.Exists(categoryName);
			}
			catch (InvalidOperationException ex)
			{
				Logger.LogErrorMessage("Category existence check failed due to exception: '{0}'", new object[]
				{
					ex
				});
			}
			return true;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000047C8 File Offset: 0x000029C8
		private static void SafeCreate(string categoryName, CounterCreationDataCollection edsCounterCollection)
		{
			try
			{
				PerformanceCounterCategory.Create(categoryName, string.Empty, PerformanceCounterCategoryType.SingleInstance, edsCounterCollection);
			}
			catch (InvalidOperationException ex)
			{
				Logger.LogErrorMessage("Counters failed to register due to exception: '{0}'", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x040002D0 RID: 720
		private const string CounterTag = "Category/Counters/Counter";

		// Token: 0x040002D1 RID: 721
		private const string CounterNameTag = "Name";

		// Token: 0x040002D2 RID: 722
		private const string CounterTypeTag = "Type";

		// Token: 0x040002D3 RID: 723
		private const string CategoryNameTag = "Category/Name";

		// Token: 0x040002D4 RID: 724
		private const string CounterXmlFileName = "Microsoft.Exchange.Diagnostics.Service.Common.EdsPerformanceCounters.xml";
	}
}
