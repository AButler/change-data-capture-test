using System;

namespace UserEditor.Models;

public record SystemAccessModel(
    int Id,
    int SystemId,
    string SystemName,
    DateTime Start,
    DateTime End
);
