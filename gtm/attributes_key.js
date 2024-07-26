
// 모든 요소를 가져온 후 'data-ep' 속성이 있는지 필터링
var elements = Array.from(document.getElementsByTagName('*')).filter(function(element) {
  return Array.from(element.attributes).some(function(attr) {
    return attr.name.startsWith('data-ga');
  });
});

// 필터링된 요소들에 클릭 이벤트 리스너 추가
elements.forEach(function(element) {
  element.addEventListener('click', function(ev) {
    window.dataLayer.push({
        ga_att: 
    })
  });
});

