# HiddenVilla2
다시 차근히 샘플구성
* 바인딩
[@on{DOM EVENT}="{DELEGATE}"]
Razor 구문을 사용하여 대리자 이벤트 처리기를 지정

// 숫자 바인딩
<input type="number" @bind-value="@Room.Price" @bind-value:event="oninput" />
input 값변경시 이벤트 [ChangeEventArgs] onchange, oninput

// CheckBox 바인딩
<input type="checkbox" @bind-value="@Room.IsActive" checked="@(Room.IsActive?"checked":null)" /> Is Active <br />

// DropDownBox 바인딩
<select @bind="SelectedRoomProp">
    @foreach (var prop in Room.RoomProps)
    {
        <option value="@prop.Name">@prop.Name</option>
    }
</select>

