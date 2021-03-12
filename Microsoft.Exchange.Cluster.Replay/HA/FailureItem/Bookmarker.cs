using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.HA.FailureItem
{
	// Token: 0x02000184 RID: 388
	internal class Bookmarker
	{
		// Token: 0x06000F7E RID: 3966 RVA: 0x00042B90 File Offset: 0x00040D90
		internal Bookmarker(Guid dbGuid)
		{
			this.m_dbGuid = dbGuid;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00042BA0 File Offset: 0x00040DA0
		internal EventBookmark Read()
		{
			this.OpenKeyIfRequired();
			EventBookmark result = null;
			if (this.m_regKey != null)
			{
				byte[] array = (byte[])this.m_regKey.GetValue(Bookmarker.valueName);
				if (array != null)
				{
					MemoryStream serializationStream = new MemoryStream(array);
					BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
					result = (EventBookmark)binaryFormatter.Deserialize(serializationStream);
				}
			}
			return result;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x00042BF4 File Offset: 0x00040DF4
		internal void Write(EventBookmark bookmark)
		{
			this.OpenKeyIfRequired();
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);
			binaryFormatter.Serialize(memoryStream, bookmark);
			if (this.m_regKey != null)
			{
				this.m_regKey.SetValue(Bookmarker.valueName, memoryStream.GetBuffer());
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00042C3A File Offset: 0x00040E3A
		internal void Delete()
		{
			this.OpenKeyIfRequired();
			if (this.m_regKey != null)
			{
				this.m_regKey.DeleteValue(Bookmarker.valueName, false);
			}
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00042C5B File Offset: 0x00040E5B
		internal void Close()
		{
			if (this.m_regKey != null)
			{
				this.m_regKey.Close();
				this.m_regKey = null;
			}
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00042C78 File Offset: 0x00040E78
		private void OpenKeyIfRequired()
		{
			if (this.m_regKey == null)
			{
				string subkey = string.Format(Bookmarker.keyFormatString, this.m_dbGuid);
				this.m_regKey = Registry.LocalMachine.CreateSubKey(subkey);
			}
		}

		// Token: 0x04000666 RID: 1638
		private static string keyFormatString = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveManager\\Databases\\{0}";

		// Token: 0x04000667 RID: 1639
		private static string valueName = "Bookmark";

		// Token: 0x04000668 RID: 1640
		private Guid m_dbGuid;

		// Token: 0x04000669 RID: 1641
		private RegistryKey m_regKey;
	}
}
