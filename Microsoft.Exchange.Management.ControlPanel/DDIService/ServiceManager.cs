using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Markup;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000160 RID: 352
	public class ServiceManager
	{
		// Token: 0x060021D3 RID: 8659 RVA: 0x00065B94 File Offset: 0x00063D94
		public static ServiceManager GetInstance()
		{
			if (ServiceManager.instance != null)
			{
				return ServiceManager.instance;
			}
			lock (ServiceManager.synchronizationObject)
			{
				if (ServiceManager.instance == null)
				{
					ServiceManager.instance = new ServiceManager();
				}
			}
			return ServiceManager.instance;
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x00065BF0 File Offset: 0x00063DF0
		protected ServiceManager()
		{
		}

		// Token: 0x17001A85 RID: 6789
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x00065C03 File Offset: 0x00063E03
		public int Count
		{
			get
			{
				return this.dic.Count;
			}
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x00065C2C File Offset: 0x00063E2C
		public Service GetService(string schemaFilesInstallPath, string schemaName)
		{
			string text = schemaName.ToLowerInvariant();
			if (!this.dic.ContainsKey(text))
			{
				lock (ServiceManager.synchronizationObject)
				{
					if (!this.dic.ContainsKey(text))
					{
						string[] files = Directory.GetFiles(schemaFilesInstallPath);
						files.Perform(delegate(string c)
						{
							this.dic[Path.GetFileNameWithoutExtension(c).ToLowerInvariant()] = null;
						});
						if (!this.dic.ContainsKey(text))
						{
							DDIHelper.Trace("Requested workflow {0} not defined", new object[]
							{
								text
							});
							throw new SchemaNotExistException(text);
						}
					}
				}
			}
			if (this.dic[text] != null)
			{
				return this.dic[text].Clone() as Service;
			}
			lock (ServiceManager.synchronizationObject)
			{
				if (this.dic[text] == null)
				{
					this.dic[text] = ServiceManager.BuildService(schemaFilesInstallPath, text);
				}
			}
			return this.dic[text].Clone() as Service;
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00065D64 File Offset: 0x00063F64
		private static Service BuildService(string schemaFilesInstallPath, string lowerCaseSchemaName)
		{
			string path = string.Format("{0}\\{1}.xaml", schemaFilesInstallPath, lowerCaseSchemaName);
			Service service = null;
			using (EcpPerformanceData.XamlParsed.StartRequestTimer())
			{
				using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
				{
					service = (XamlReader.Load(fileStream) as Service);
				}
			}
			service.Initialize();
			return service;
		}

		// Token: 0x04001D4C RID: 7500
		private static object synchronizationObject = new object();

		// Token: 0x04001D4D RID: 7501
		private static ServiceManager instance;

		// Token: 0x04001D4E RID: 7502
		private Dictionary<string, Service> dic = new Dictionary<string, Service>();
	}
}
