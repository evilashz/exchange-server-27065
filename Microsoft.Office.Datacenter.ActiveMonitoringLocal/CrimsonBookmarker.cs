using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000075 RID: 117
	public class CrimsonBookmarker : IEventBookmarker
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x0001C247 File Offset: 0x0001A447
		public CrimsonBookmarker()
		{
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001C24F File Offset: 0x0001A44F
		public CrimsonBookmarker(string baseLocation)
		{
			this.Initialize(baseLocation);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001C25E File Offset: 0x0001A45E
		public void Initialize(string baseLocation)
		{
			this.baseLocation = baseLocation;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001C268 File Offset: 0x0001A468
		public EventBookmark Read(string bookmarkName)
		{
			EventBookmark result = null;
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(this.baseLocation))
			{
				if (registryKey != null)
				{
					byte[] array = (byte[])registryKey.GetValue(bookmarkName);
					if (array != null)
					{
						using (MemoryStream memoryStream = new MemoryStream(array))
						{
							BinaryFormatter binaryFormatter = new BinaryFormatter();
							result = (EventBookmark)binaryFormatter.Deserialize(memoryStream);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
		public void Write(string bookmarkName, EventBookmark bookmark)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(this.baseLocation))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					binaryFormatter.Serialize(memoryStream, bookmark);
					registryKey.SetValue(bookmarkName, memoryStream.GetBuffer());
				}
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001C364 File Offset: 0x0001A564
		public void Delete(string bookmarkName)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(this.baseLocation))
			{
				registryKey.DeleteValue(bookmarkName, false);
			}
		}

		// Token: 0x04000447 RID: 1095
		private string baseLocation;
	}
}
