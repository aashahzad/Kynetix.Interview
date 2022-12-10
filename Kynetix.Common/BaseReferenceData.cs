using System;

namespace Kynetix.Common;

public class BaseReferenceData
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class Exchange : BaseReferenceData
{
}

public class Firm : BaseReferenceData
{
}

public class Account : BaseReferenceData
{
}

public class Instrument : BaseReferenceData
{
}

public class Currency : BaseReferenceData
{
}
