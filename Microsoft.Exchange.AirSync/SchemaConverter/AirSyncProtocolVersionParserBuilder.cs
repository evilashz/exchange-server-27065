using System;

namespace Microsoft.Exchange.AirSync.SchemaConverter
{
	// Token: 0x020001BC RID: 444
	internal class AirSyncProtocolVersionParserBuilder
	{
		// Token: 0x060012C0 RID: 4800 RVA: 0x00063994 File Offset: 0x00061B94
		public static IAirSyncVersionFactory FromVersion(int version)
		{
			if (version <= 25)
			{
				switch (version)
				{
				case 20:
					return new AirSyncVersionFactoryV20();
				case 21:
					return new AirSyncVersionFactoryV21();
				default:
					if (version == 25)
					{
						return new AirSyncVersionFactoryV25();
					}
					break;
				}
			}
			else
			{
				switch (version)
				{
				case 120:
					return new AirSyncVersionFactoryV120();
				case 121:
					return new AirSyncVersionFactoryV121();
				default:
					switch (version)
					{
					case 140:
						return new AirSyncVersionFactoryV140();
					case 141:
						return new AirSyncVersionFactoryV141();
					default:
						if (version == 160)
						{
							return new AirSyncVersionFactoryV160();
						}
						break;
					}
					break;
				}
			}
			throw new NotImplementedException("AirSync version " + version + " is not supported!");
		}
	}
}
