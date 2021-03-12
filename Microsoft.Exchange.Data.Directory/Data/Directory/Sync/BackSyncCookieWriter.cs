using System;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007B3 RID: 1971
	internal class BackSyncCookieWriter : IDisposable
	{
		// Token: 0x060061F7 RID: 25079 RVA: 0x0014F7A8 File Offset: 0x0014D9A8
		internal static BackSyncCookieWriter Create(Type cookieType)
		{
			int num = 0;
			BackSyncCookieAttribute[] cookieAttributeDefinitions = null;
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Cookie Type {0}", cookieType.Name);
			BackSyncCookieAttribute.CreateBackSyncCookieAttributeDefinitions(null, cookieType, out num, out cookieAttributeDefinitions);
			return new BackSyncCookieWriter(cookieAttributeDefinitions);
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x0014F7E8 File Offset: 0x0014D9E8
		internal BackSyncCookieWriter(BackSyncCookieAttribute[] cookieAttributeDefinitions)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "New BackSyncCookieReader");
			this.attributeDefinitions = cookieAttributeDefinitions;
			this.cookieMemoryStream = new MemoryStream();
			this.cookieBinaryWriter = new BinaryWriter(this.cookieMemoryStream);
			this.disposed = false;
			this.currentAttributeIndex = 0;
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x0014F841 File Offset: 0x0014DA41
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060061FA RID: 25082 RVA: 0x0014F850 File Offset: 0x0014DA50
		internal void WriteNextAttributeValue(object attributeValue)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Write next attribute value");
			BackSyncCookieAttribute backSyncCookieAttribute = this.attributeDefinitions[this.currentAttributeIndex];
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Attribute {0}", backSyncCookieAttribute.ToString());
			if (backSyncCookieAttribute.DataType.Equals(typeof(int)))
			{
				this.cookieBinaryWriter.Write((int)attributeValue);
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "attributeValue (Int32) = {0}", new object[]
				{
					attributeValue
				});
			}
			else if (backSyncCookieAttribute.DataType.Equals(typeof(long)))
			{
				this.cookieBinaryWriter.Write((long)attributeValue);
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "attributeValue (Int64) = {0}", new object[]
				{
					attributeValue
				});
			}
			else if (backSyncCookieAttribute.DataType.Equals(typeof(bool)))
			{
				this.cookieBinaryWriter.Write((bool)attributeValue);
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "attributeValue (Boolean) = {0}", new object[]
				{
					attributeValue
				});
			}
			else if (backSyncCookieAttribute.DataType.Equals(typeof(Guid)))
			{
				Guid arg = (Guid)attributeValue;
				this.cookieBinaryWriter.Write(arg.ToByteArray());
				ExTraceGlobals.BackSyncTracer.TraceDebug<Guid>((long)SyncConfiguration.TraceId, "attributeValue (Guid) = {0}", arg);
			}
			else if (backSyncCookieAttribute.DataType.Equals(typeof(string)))
			{
				if (attributeValue != null)
				{
					this.cookieBinaryWriter.Write((string)attributeValue);
					ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "attributeValue (string) = {0}", new object[]
					{
						attributeValue
					});
				}
				else
				{
					this.cookieBinaryWriter.Write(string.Empty);
					ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "attributeValue (string) = \"\"");
				}
			}
			else if (backSyncCookieAttribute.DataType.Equals(typeof(byte[])))
			{
				if (attributeValue != null)
				{
					byte[] array = (byte[])attributeValue;
					this.cookieBinaryWriter.Write(array.Length);
					this.cookieBinaryWriter.Write(array);
					ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "attributeValue (byte[]) size = {0}", array.Length);
					ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "  Base64String {0}", Convert.ToBase64String(array));
				}
				else
				{
					this.cookieBinaryWriter.Write(0);
					ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "attributeValue (byte[]) size = 0");
				}
			}
			else
			{
				if (!backSyncCookieAttribute.DataType.Equals(typeof(string[])))
				{
					ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "Invalid attribute data type {0}", backSyncCookieAttribute.DataType.Name);
					throw new InvalidCookieException();
				}
				if (attributeValue != null)
				{
					string[] array2 = (string[])attributeValue;
					this.cookieBinaryWriter.Write(array2.Length);
					ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "attributeValue (string[]) size = {0}", array2.Length);
					foreach (string text in array2)
					{
						this.cookieBinaryWriter.Write(text);
						ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "  value[] = \"{0}\"", text);
					}
				}
				else
				{
					this.cookieBinaryWriter.Write(0);
					ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "attributeValue (string[]) size = 0");
				}
			}
			this.currentAttributeIndex++;
		}

		// Token: 0x060061FB RID: 25083 RVA: 0x0014FBDC File Offset: 0x0014DDDC
		internal byte[] GetBinaryCookie()
		{
			byte[] array = this.cookieMemoryStream.ToArray();
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Return binary cookie \"{0}\"", (array != null) ? Convert.ToBase64String(array) : "NULL");
			return array;
		}

		// Token: 0x060061FC RID: 25084 RVA: 0x0014FC1C File Offset: 0x0014DE1C
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Dispose BackSyncCookieWriter");
					if (this.cookieBinaryWriter != null)
					{
						this.cookieBinaryWriter.Close();
					}
					if (this.cookieMemoryStream != null)
					{
						this.cookieMemoryStream.Close();
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x040041AA RID: 16810
		private int currentAttributeIndex;

		// Token: 0x040041AB RID: 16811
		private BackSyncCookieAttribute[] attributeDefinitions;

		// Token: 0x040041AC RID: 16812
		private MemoryStream cookieMemoryStream;

		// Token: 0x040041AD RID: 16813
		private BinaryWriter cookieBinaryWriter;

		// Token: 0x040041AE RID: 16814
		private bool disposed;
	}
}
