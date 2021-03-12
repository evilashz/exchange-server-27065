using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007B2 RID: 1970
	internal class BackSyncCookieReader : IDisposable
	{
		// Token: 0x060061F2 RID: 25074 RVA: 0x0014F300 File Offset: 0x0014D500
		internal static BackSyncCookieReader Create(byte[] binaryCookie, Type cookieType)
		{
			int attributeCount = 0;
			BackSyncCookieAttribute[] cookieAttributeDefinitions = null;
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Cookie Type {0}", cookieType.Name);
			BackSyncCookieAttribute.CreateBackSyncCookieAttributeDefinitions(binaryCookie, cookieType, out attributeCount, out cookieAttributeDefinitions);
			return new BackSyncCookieReader(attributeCount, cookieAttributeDefinitions, binaryCookie);
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x0014F344 File Offset: 0x0014D544
		internal BackSyncCookieReader(int attributeCount, BackSyncCookieAttribute[] cookieAttributeDefinitions, byte[] binaryCookie)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "New BackSyncCookieReader");
			this.cookieAttributeCount = attributeCount;
			this.currentAttributeIndex = 0;
			this.attributeDefinitions = cookieAttributeDefinitions;
			this.cookieMemoryStream = new MemoryStream(binaryCookie);
			this.cookieBinaryReader = new BinaryReader(this.cookieMemoryStream);
			this.disposed = false;
		}

		// Token: 0x060061F4 RID: 25076 RVA: 0x0014F3A5 File Offset: 0x0014D5A5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060061F5 RID: 25077 RVA: 0x0014F3B4 File Offset: 0x0014D5B4
		internal object GetNextAttributeValue()
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Get next attribute value");
			BackSyncCookieAttribute backSyncCookieAttribute = this.attributeDefinitions[this.currentAttributeIndex];
			ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "Attribute {0}", backSyncCookieAttribute.ToString());
			object obj = backSyncCookieAttribute.DefaultValue;
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "this.cookieAttributeCount = {0}", this.cookieAttributeCount);
			ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "this.currentAttributeIndex = {0}", this.currentAttributeIndex);
			if (this.currentAttributeIndex < this.cookieAttributeCount)
			{
				if (backSyncCookieAttribute.DataType.Equals(typeof(int)))
				{
					obj = this.cookieBinaryReader.ReadInt32();
					ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "attributeValue (Int32) = {0}", new object[]
					{
						obj
					});
				}
				else if (backSyncCookieAttribute.DataType.Equals(typeof(long)))
				{
					obj = this.cookieBinaryReader.ReadInt64();
					ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "attributeValue (Int64) = {0}", new object[]
					{
						obj
					});
				}
				else if (backSyncCookieAttribute.DataType.Equals(typeof(bool)))
				{
					obj = this.cookieBinaryReader.ReadBoolean();
					ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "attributeValue (Boolean) = {0}", new object[]
					{
						obj
					});
				}
				else if (backSyncCookieAttribute.DataType.Equals(typeof(Guid)))
				{
					obj = new Guid(this.cookieBinaryReader.ReadBytes(16));
					ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "attributeValue (Guid) = {0}", new object[]
					{
						obj
					});
				}
				else if (backSyncCookieAttribute.DataType.Equals(typeof(string)))
				{
					obj = this.cookieBinaryReader.ReadString();
					ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "attributeValue (string) = {0}", new object[]
					{
						obj
					});
				}
				else if (backSyncCookieAttribute.DataType.Equals(typeof(byte[])))
				{
					int num = this.cookieBinaryReader.ReadInt32();
					ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "attributeValue (bypte[]) size = {0}", num);
					if (num > 0)
					{
						obj = this.cookieBinaryReader.ReadBytes(num);
						ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, "  Base64String {0}", Convert.ToBase64String((byte[])obj));
					}
					else
					{
						obj = null;
					}
				}
				else
				{
					if (!backSyncCookieAttribute.DataType.Equals(typeof(string[])))
					{
						ExTraceGlobals.BackSyncTracer.TraceError<string>((long)SyncConfiguration.TraceId, "Invalid attribute data type {0}", backSyncCookieAttribute.DataType.Name);
						throw new InvalidCookieException();
					}
					int num2 = this.cookieBinaryReader.ReadInt32();
					ExTraceGlobals.BackSyncTracer.TraceDebug<int>((long)SyncConfiguration.TraceId, "attributeValue (string[]) size = {0}", num2);
					if (num2 > 0)
					{
						List<string> list = new List<string>();
						for (int i = 0; i < num2; i++)
						{
							string text = this.cookieBinaryReader.ReadString();
							ExTraceGlobals.BackSyncTracer.TraceDebug<int, string>((long)SyncConfiguration.TraceId, "  value[{0}] = \"{1}\"", i, text);
							list.Add(text);
						}
						obj = list.ToArray();
					}
					else
					{
						obj = null;
					}
				}
			}
			else
			{
				obj = backSyncCookieAttribute.DefaultValue;
				ExTraceGlobals.BackSyncTracer.TraceDebug<string>((long)SyncConfiguration.TraceId, " attributeValue (Default) = {0}", (obj != null) ? obj.ToString() : "NULL");
			}
			this.currentAttributeIndex++;
			return obj;
		}

		// Token: 0x060061F6 RID: 25078 RVA: 0x0014F74C File Offset: 0x0014D94C
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "Dispose BackSyncCookieReader");
					if (this.cookieBinaryReader != null)
					{
						this.cookieBinaryReader.Close();
					}
					if (this.cookieMemoryStream != null)
					{
						this.cookieMemoryStream.Close();
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x040041A4 RID: 16804
		private readonly int cookieAttributeCount;

		// Token: 0x040041A5 RID: 16805
		private int currentAttributeIndex;

		// Token: 0x040041A6 RID: 16806
		private BackSyncCookieAttribute[] attributeDefinitions;

		// Token: 0x040041A7 RID: 16807
		private MemoryStream cookieMemoryStream;

		// Token: 0x040041A8 RID: 16808
		private BinaryReader cookieBinaryReader;

		// Token: 0x040041A9 RID: 16809
		private bool disposed;
	}
}
