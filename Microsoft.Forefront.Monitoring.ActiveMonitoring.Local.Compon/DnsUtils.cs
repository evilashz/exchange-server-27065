using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000031 RID: 49
	public static class DnsUtils
	{
		// Token: 0x06000164 RID: 356 RVA: 0x0000AC44 File Offset: 0x00008E44
		public static DnsUtils.DnsResponse GetARecord(string domain)
		{
			int returnCode;
			DnsUtils.ARecord arecord = (from r in DnsUtils.GetDNSRecords(domain, DnsUtils.DnsRecordType.DnsTypeA, null, out returnCode)
			where r.Type == DnsUtils.DnsRecordType.DnsTypeA
			select r).Cast<DnsUtils.ARecord>().FirstOrDefault<DnsUtils.ARecord>();
			IPAddress ipAddress = (arecord == null) ? IPAddress.None : arecord.IpAddress;
			return new DnsUtils.DnsResponse(returnCode, ipAddress);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000ACAC File Offset: 0x00008EAC
		public static DnsUtils.DnsResponse GetAAAARecord(string domain)
		{
			int returnCode;
			DnsUtils.AAAARecord aaaarecord = (from r in DnsUtils.GetDNSRecords(domain, DnsUtils.DnsRecordType.DnsTypeAAAA, null, out returnCode)
			where r.Type == DnsUtils.DnsRecordType.DnsTypeAAAA
			select r).Cast<DnsUtils.AAAARecord>().FirstOrDefault<DnsUtils.AAAARecord>();
			IPAddress ipAddress = (aaaarecord == null) ? IPAddress.None : aaaarecord.IpAddress;
			return new DnsUtils.DnsResponse(returnCode, ipAddress);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000AD11 File Offset: 0x00008F11
		public static string[] GetMXRecords(string domain, out int returnCode)
		{
			return (from DnsUtils.MXRecord mx in DnsUtils.GetDNSRecords(domain, DnsUtils.DnsRecordType.DnsTypeMX, null, out returnCode)
			select mx.NameExchange).ToArray<string>();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000AD70 File Offset: 0x00008F70
		public static DnsUtils.DnsResponse GetMXEndPointForDomain(string domain)
		{
			int returnCode;
			IEnumerable<DnsUtils.DNSRecord> dnsrecords = DnsUtils.GetDNSRecords(domain, DnsUtils.DnsRecordType.DnsTypeMX, new DnsUtils.DnsRecordType[]
			{
				DnsUtils.DnsRecordType.DnsTypeA
			}, out returnCode);
			IEnumerable<DnsUtils.ARecord> source = (from r in dnsrecords
			where r.Type == DnsUtils.DnsRecordType.DnsTypeA
			select r).Cast<DnsUtils.ARecord>();
			if (source.Count<DnsUtils.ARecord>() > 0)
			{
				return new DnsUtils.DnsResponse(returnCode, source.First<DnsUtils.ARecord>().IpAddress);
			}
			foreach (string domain2 in from DnsUtils.MXRecord mx in 
				from r in dnsrecords
				where r.Type == DnsUtils.DnsRecordType.DnsTypeMX
				select r
			select mx.NameExchange)
			{
				IEnumerable<DnsUtils.ARecord> source2 = DnsUtils.GetDNSRecords(domain2, DnsUtils.DnsRecordType.DnsTypeA, null, out returnCode).Cast<DnsUtils.ARecord>();
				if (source2.Count<DnsUtils.ARecord>() > 0)
				{
					return new DnsUtils.DnsResponse(returnCode, source2.Select((DnsUtils.ARecord a) => a.IpAddress).First<IPAddress>());
				}
			}
			source = DnsUtils.GetDNSRecords(domain, DnsUtils.DnsRecordType.DnsTypeA, null, out returnCode).Cast<DnsUtils.ARecord>();
			if (source.Count<DnsUtils.ARecord>() > 0)
			{
				return new DnsUtils.DnsResponse(returnCode, source.First<DnsUtils.ARecord>().IpAddress);
			}
			return new DnsUtils.DnsResponse(returnCode, IPAddress.None);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000AF14 File Offset: 0x00009114
		public static IEnumerable<IPAddress> GetNSIpEndPointsForDomain(string domain)
		{
			int num;
			IEnumerable<DnsUtils.DNSRecord> dnsrecords = DnsUtils.GetDNSRecords(domain, DnsUtils.DnsRecordType.DnsTypeNS, new DnsUtils.DnsRecordType[]
			{
				DnsUtils.DnsRecordType.DnsTypeA
			}, out num);
			IEnumerable<DnsUtils.ARecord> source = (from r in dnsrecords
			where r.Type == DnsUtils.DnsRecordType.DnsTypeA
			select r).Cast<DnsUtils.ARecord>();
			if (source.Count<DnsUtils.ARecord>() > 0)
			{
				return from a in source
				select a.IpAddress;
			}
			List<IPAddress> list = new List<IPAddress>();
			foreach (string domain2 in from DnsUtils.NSRecord ns in 
				from r in dnsrecords
				where r.Type == DnsUtils.DnsRecordType.DnsTypeNS
				select r
			select ns.NameHost)
			{
				IEnumerable<DnsUtils.ARecord> source2 = DnsUtils.GetDNSRecords(domain2, DnsUtils.DnsRecordType.DnsTypeA, null, out num).Cast<DnsUtils.ARecord>();
				list.AddRange(source2.Select((DnsUtils.ARecord a) => a.IpAddress));
			}
			return list;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B058 File Offset: 0x00009258
		private static IEnumerable<DnsUtils.DNSRecord> GetDNSRecords(string domain, DnsUtils.DnsRecordType queryType, IEnumerable<DnsUtils.DnsRecordType> optionalRecordTypes, out int returnCode)
		{
			IntPtr zero = IntPtr.Zero;
			HashSet<ushort> hashSet;
			if (optionalRecordTypes == null)
			{
				hashSet = new HashSet<ushort>();
			}
			else
			{
				hashSet = new HashSet<ushort>(optionalRecordTypes.Cast<ushort>());
			}
			IEnumerable<DnsUtils.DNSRecord> recordsFromResponse;
			try
			{
				returnCode = DnsUtils.DnsQuery(ref domain, queryType, DnsUtils.DnsQueryOption.DnsQueryBypassCache, 0, ref zero, 0);
				if (returnCode == 0)
				{
					hashSet.Add((ushort)queryType);
				}
				recordsFromResponse = DnsUtils.GetRecordsFromResponse(zero, hashSet);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					DnsUtils.DnsRecordListFree(zero, 0);
				}
			}
			return recordsFromResponse;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000B0D0 File Offset: 0x000092D0
		private static IEnumerable<DnsUtils.DNSRecord> GetRecordsFromResponse(IntPtr responsePtr, HashSet<ushort> recordTypes)
		{
			List<DnsUtils.DNSRecord> list = new List<DnsUtils.DNSRecord>();
			IntPtr intPtr = responsePtr;
			while (intPtr != IntPtr.Zero)
			{
				DnsUtils.DNSHeaderLayout dnsheaderLayout = (DnsUtils.DNSHeaderLayout)Marshal.PtrToStructure(intPtr, typeof(DnsUtils.DNSHeaderLayout));
				if (recordTypes.Contains(dnsheaderLayout.Type))
				{
					if (dnsheaderLayout.Type == 15)
					{
						DnsUtils.MXRecordLayout mxRecord = (DnsUtils.MXRecordLayout)Marshal.PtrToStructure(intPtr, typeof(DnsUtils.MXRecordLayout));
						list.Add(new DnsUtils.MXRecord(mxRecord));
					}
					else if (dnsheaderLayout.Type == 2)
					{
						DnsUtils.NSRecordLayout nsRecord = (DnsUtils.NSRecordLayout)Marshal.PtrToStructure(intPtr, typeof(DnsUtils.NSRecordLayout));
						list.Add(new DnsUtils.NSRecord(nsRecord));
					}
					else if (dnsheaderLayout.Type == 1)
					{
						DnsUtils.ARecordLayout aRecord = (DnsUtils.ARecordLayout)Marshal.PtrToStructure(intPtr, typeof(DnsUtils.ARecordLayout));
						list.Add(new DnsUtils.ARecord(aRecord));
					}
					else
					{
						if (dnsheaderLayout.Type != 28)
						{
							throw new NotImplementedException(string.Format("RecordType ={0} not implemented", dnsheaderLayout.Type));
						}
						DnsUtils.AAAARecordLayout aaaaRecord = (DnsUtils.AAAARecordLayout)Marshal.PtrToStructure(intPtr, typeof(DnsUtils.AAAARecordLayout));
						list.Add(new DnsUtils.AAAARecord(aaaaRecord));
					}
				}
				intPtr = dnsheaderLayout.Next;
			}
			return list;
		}

		// Token: 0x0600016B RID: 363
		[DllImport("dnsapi", CharSet = CharSet.Unicode, EntryPoint = "DnsQuery_W", ExactSpelling = true, SetLastError = true)]
		private static extern int DnsQuery([MarshalAs(UnmanagedType.VBByRefStr)] ref string pszName, DnsUtils.DnsRecordType wType, DnsUtils.DnsQueryOption options, int aipServers, ref IntPtr ppQueryResults, int pReserved);

		// Token: 0x0600016C RID: 364
		[DllImport("dnsapi", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern void DnsRecordListFree(IntPtr recordList, int freeType);

		// Token: 0x02000032 RID: 50
		public enum DnsRecordType : ushort
		{
			// Token: 0x040000F1 RID: 241
			DnsTypeA = 1,
			// Token: 0x040000F2 RID: 242
			DnsTypeNS,
			// Token: 0x040000F3 RID: 243
			DnsTypeMX = 15,
			// Token: 0x040000F4 RID: 244
			DnsTypeAAAA = 28
		}

		// Token: 0x02000033 RID: 51
		public enum DnsResponseCode : ushort
		{
			// Token: 0x040000F6 RID: 246
			DNS_ERROR_RCODE_NO_ERROR,
			// Token: 0x040000F7 RID: 247
			DNS_ERROR_RCODE_FORMAT_ERROR = 9001,
			// Token: 0x040000F8 RID: 248
			DNS_ERROR_RCODE_SERVER_FAILURE,
			// Token: 0x040000F9 RID: 249
			DNS_ERROR_RCODE_NAME_ERROR,
			// Token: 0x040000FA RID: 250
			DNS_ERROR_RCODE_NOT_IMPLEMENTED,
			// Token: 0x040000FB RID: 251
			DNS_ERROR_RCODE_REFUSED,
			// Token: 0x040000FC RID: 252
			DNS_ERROR_RCODE_YXDOMAIN,
			// Token: 0x040000FD RID: 253
			DNS_ERROR_RCODE_YXRRSET,
			// Token: 0x040000FE RID: 254
			DNS_ERROR_RCODE_NXRRSET,
			// Token: 0x040000FF RID: 255
			DNS_ERROR_RCODE_NOTAUTH,
			// Token: 0x04000100 RID: 256
			DNS_ERROR_RCODE_NOTZONE,
			// Token: 0x04000101 RID: 257
			DNS_ERROR_RCODE_BADSIG = 9016,
			// Token: 0x04000102 RID: 258
			DNS_ERROR_RCODE_BADKEY,
			// Token: 0x04000103 RID: 259
			DNS_ERROR_RCODE_BADTIME,
			// Token: 0x04000104 RID: 260
			ERROR_TIMEOUT = 1460
		}

		// Token: 0x02000034 RID: 52
		private enum DnsQueryOption
		{
			// Token: 0x04000106 RID: 262
			DnsQueryAcceptTruncatedResponse = 1,
			// Token: 0x04000107 RID: 263
			DnsQueryBypassCache = 8,
			// Token: 0x04000108 RID: 264
			DnsQueryDontResetTtlValues = 1048576,
			// Token: 0x04000109 RID: 265
			DnsQueryNoHostsFile = 64,
			// Token: 0x0400010A RID: 266
			DnsQueryNoLocalName = 32,
			// Token: 0x0400010B RID: 267
			DnsQueryNoNetBT = 128,
			// Token: 0x0400010C RID: 268
			DnsQueryNoRecursion = 4,
			// Token: 0x0400010D RID: 269
			DnsQueryNoWireQuery = 16,
			// Token: 0x0400010E RID: 270
			DnsQueryReserved = -16777216,
			// Token: 0x0400010F RID: 271
			DnsQueryReturnMessage = 512,
			// Token: 0x04000110 RID: 272
			DnsQueryStandard = 0,
			// Token: 0x04000111 RID: 273
			DnsQueryTreatAsFqdn = 4096,
			// Token: 0x04000112 RID: 274
			DnsQueryUseTcpOnly = 2,
			// Token: 0x04000113 RID: 275
			DnsQueryWireOnly = 256
		}

		// Token: 0x02000035 RID: 53
		private struct DNSHeaderLayout
		{
			// Token: 0x04000114 RID: 276
			public IntPtr Next;

			// Token: 0x04000115 RID: 277
			public string Name;

			// Token: 0x04000116 RID: 278
			public ushort Type;

			// Token: 0x04000117 RID: 279
			public ushort DataLength;

			// Token: 0x04000118 RID: 280
			public uint Flags;

			// Token: 0x04000119 RID: 281
			public uint Ttl;

			// Token: 0x0400011A RID: 282
			public uint Reserved;
		}

		// Token: 0x02000036 RID: 54
		private struct MXRecordLayout
		{
			// Token: 0x0400011B RID: 283
			public DnsUtils.DNSHeaderLayout Header;

			// Token: 0x0400011C RID: 284
			public IntPtr NameExchange;

			// Token: 0x0400011D RID: 285
			public ushort Preference;

			// Token: 0x0400011E RID: 286
			public ushort Pad;
		}

		// Token: 0x02000037 RID: 55
		private struct NSRecordLayout
		{
			// Token: 0x0400011F RID: 287
			public DnsUtils.DNSHeaderLayout Header;

			// Token: 0x04000120 RID: 288
			public IntPtr NameHost;
		}

		// Token: 0x02000038 RID: 56
		private struct ARecordLayout
		{
			// Token: 0x04000121 RID: 289
			public DnsUtils.DNSHeaderLayout Header;

			// Token: 0x04000122 RID: 290
			public uint IpAddress;
		}

		// Token: 0x02000039 RID: 57
		private struct AAAARecordLayout
		{
			// Token: 0x04000123 RID: 291
			public DnsUtils.DNSHeaderLayout Header;

			// Token: 0x04000124 RID: 292
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] IpAddress;
		}

		// Token: 0x0200003A RID: 58
		public class DnsResponse
		{
			// Token: 0x06000179 RID: 377 RVA: 0x0000B20A File Offset: 0x0000940A
			public DnsResponse(int returnCode, IPAddress ipAddress)
			{
				this.ReturnCode = (DnsUtils.DnsResponseCode)returnCode;
				this.IPAddress = ipAddress;
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x0600017A RID: 378 RVA: 0x0000B221 File Offset: 0x00009421
			// (set) Token: 0x0600017B RID: 379 RVA: 0x0000B229 File Offset: 0x00009429
			public DnsUtils.DnsResponseCode ReturnCode { get; private set; }

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x0600017C RID: 380 RVA: 0x0000B232 File Offset: 0x00009432
			// (set) Token: 0x0600017D RID: 381 RVA: 0x0000B23A File Offset: 0x0000943A
			public IPAddress IPAddress { get; private set; }

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x0600017E RID: 382 RVA: 0x0000B243 File Offset: 0x00009443
			public bool DnsResolvedSuccessfuly
			{
				get
				{
					return this.IPAddress != IPAddress.None;
				}
			}
		}

		// Token: 0x0200003B RID: 59
		private class DNSRecord
		{
			// Token: 0x0600017F RID: 383 RVA: 0x0000B255 File Offset: 0x00009455
			public DNSRecord(DnsUtils.DNSHeaderLayout recordHeader)
			{
				this.Name = recordHeader.Name;
				this.Type = (DnsUtils.DnsRecordType)recordHeader.Type;
				this.Flags = recordHeader.Flags;
				this.Ttl = recordHeader.Ttl;
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000180 RID: 384 RVA: 0x0000B291 File Offset: 0x00009491
			// (set) Token: 0x06000181 RID: 385 RVA: 0x0000B299 File Offset: 0x00009499
			public string Name { get; private set; }

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000182 RID: 386 RVA: 0x0000B2A2 File Offset: 0x000094A2
			// (set) Token: 0x06000183 RID: 387 RVA: 0x0000B2AA File Offset: 0x000094AA
			public DnsUtils.DnsRecordType Type { get; private set; }

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x06000184 RID: 388 RVA: 0x0000B2B3 File Offset: 0x000094B3
			// (set) Token: 0x06000185 RID: 389 RVA: 0x0000B2BB File Offset: 0x000094BB
			public uint Flags { get; private set; }

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x06000186 RID: 390 RVA: 0x0000B2C4 File Offset: 0x000094C4
			// (set) Token: 0x06000187 RID: 391 RVA: 0x0000B2CC File Offset: 0x000094CC
			public uint Ttl { get; private set; }
		}

		// Token: 0x0200003C RID: 60
		private class MXRecord : DnsUtils.DNSRecord
		{
			// Token: 0x06000188 RID: 392 RVA: 0x0000B2D5 File Offset: 0x000094D5
			public MXRecord(DnsUtils.MXRecordLayout mxRecord) : base(mxRecord.Header)
			{
				this.NameExchange = Marshal.PtrToStringAuto(mxRecord.NameExchange);
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x06000189 RID: 393 RVA: 0x0000B2F6 File Offset: 0x000094F6
			// (set) Token: 0x0600018A RID: 394 RVA: 0x0000B2FE File Offset: 0x000094FE
			public string NameExchange { get; private set; }
		}

		// Token: 0x0200003D RID: 61
		private class ARecord : DnsUtils.DNSRecord
		{
			// Token: 0x0600018B RID: 395 RVA: 0x0000B307 File Offset: 0x00009507
			public ARecord(DnsUtils.ARecordLayout aRecord) : base(aRecord.Header)
			{
				this.IpAddress = new IPAddress((long)((ulong)aRecord.IpAddress));
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x0600018C RID: 396 RVA: 0x0000B329 File Offset: 0x00009529
			// (set) Token: 0x0600018D RID: 397 RVA: 0x0000B331 File Offset: 0x00009531
			public IPAddress IpAddress { get; private set; }
		}

		// Token: 0x0200003E RID: 62
		private class AAAARecord : DnsUtils.DNSRecord
		{
			// Token: 0x0600018E RID: 398 RVA: 0x0000B33A File Offset: 0x0000953A
			public AAAARecord(DnsUtils.AAAARecordLayout aaaaRecord) : base(aaaaRecord.Header)
			{
				this.IpAddress = new IPAddress(aaaaRecord.IpAddress);
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x0600018F RID: 399 RVA: 0x0000B35B File Offset: 0x0000955B
			// (set) Token: 0x06000190 RID: 400 RVA: 0x0000B363 File Offset: 0x00009563
			public IPAddress IpAddress { get; private set; }
		}

		// Token: 0x0200003F RID: 63
		private class NSRecord : DnsUtils.DNSRecord
		{
			// Token: 0x06000191 RID: 401 RVA: 0x0000B36C File Offset: 0x0000956C
			public NSRecord(DnsUtils.NSRecordLayout nsRecord) : base(nsRecord.Header)
			{
				this.NameHost = Marshal.PtrToStringAuto(nsRecord.NameHost);
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x06000192 RID: 402 RVA: 0x0000B38D File Offset: 0x0000958D
			// (set) Token: 0x06000193 RID: 403 RVA: 0x0000B395 File Offset: 0x00009595
			public string NameHost { get; private set; }
		}
	}
}
