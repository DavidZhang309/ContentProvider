vimeoModule = { }

function vimeoModule.Browse(type, page)
	print ("start, " .. "https://vimeo.com/search/page:" .. page .. "?q=" .. type)
	webData = downloadString("https://vimeo.com/search/page:" .. page .. "?q=" .. type)
	--webData = downloadString("https://google.ca")
	listing = { }
	currentIndex = string.find(webData, "<span class=\"title\" title=\"")
	while currentIndex ~= -1 do
		title = string.extract(webData, "<span class=\"title\" title=\"", "\"", currentIndex)
		img = string.extract(webData, "<img src=\"", "\"", currentIndex)
		link = string.extract(webData, "a href=\"", "\"", string.find("<li", currentIndex))
		table.insert(listing, data.createSeriesInfo(title, link, img))
		currentIndex = string.find(webData, "<span class=\"title\" title=\"", currentIndex + 1)
	end
	return listing
end

function vimeoModule.GetList(path)

end

function vimeoModule.GetLink(path)

end

return vimeoModule