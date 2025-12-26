#!/bin/bash

# Verification script for Baby Feeding Log application

echo "=========================================="
echo "Baby Feeding Log - Verification Script"
echo "=========================================="
echo ""

# Check if .NET is available
echo "1. Checking .NET SDK..."
if command -v dotnet &> /dev/null; then
    echo "   ✓ .NET SDK found: $(dotnet --version)"
else
    echo "   ✗ .NET SDK not found"
    exit 1
fi

echo ""
echo "2. Building the application..."
cd "$(dirname "$0")/BabyFeedingLog"
if dotnet build -c Release > /dev/null 2>&1; then
    echo "   ✓ Build successful"
else
    echo "   ✗ Build failed"
    exit 1
fi

echo ""
echo "3. Checking project structure..."
required_files=(
    "BabyFeedingLog.csproj"
    "Program.cs"
    "Form1.cs"
    "Form1.Designer.cs"
    "Models/InfantInfo.cs"
    "Models/FeedingEntry.cs"
    "Models/DiaperEntry.cs"
    "Models/SleepEntry.cs"
    "Models/GrowthEntry.cs"
    "Services/DataManager.cs"
)

for file in "${required_files[@]}"; do
    if [ -f "$file" ]; then
        echo "   ✓ $file"
    else
        echo "   ✗ $file (missing)"
        exit 1
    fi
done

echo ""
echo "4. Checking build output..."
if [ -f "bin/Release/net8.0-windows/BabyFeedingLog.dll" ]; then
    echo "   ✓ Application DLL built successfully"
else
    echo "   ✗ Application DLL not found"
    exit 1
fi

echo ""
echo "=========================================="
echo "All checks passed! ✓"
echo "=========================================="
echo ""
echo "Application Information:"
echo "  - Name: Baby Feeding Log"
echo "  - Target: Windows (.NET 8.0)"
echo "  - Features: Feeding, Diaper, Sleep, Growth tracking"
echo ""
echo "To run the application:"
echo "  cd BabyFeedingLog && dotnet run"
echo ""
