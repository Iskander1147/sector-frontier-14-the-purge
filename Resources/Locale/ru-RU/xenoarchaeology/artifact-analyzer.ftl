analysis-console-menu-title = аналитическая консоль
analysis-console-server-list-button = Список серверов
analysis-console-extract-button = Извлечь О.И.

analysis-console-info-no-scanner = Анализатор не подключён! Пожалуйста, подключите его с помощью мультитула.
analysis-console-info-no-artifact = Артефакт не найден! Поместите артефакт на платформу, затем просканируйте для получения данных.
analysis-console-info-ready = Все системы запущены. Сканирование готово.

analysis-console-no-node = Выберите узел для просмотра.
analysis-console-info-id = [font="Monospace" size=11]ID_УЗЛА:[/font]
analysis-console-info-id-value = [font="Monospace" size=11][color=yellow]{$id}[/color][/font]
analysis-console-info-class = [font="Monospace" size=11]Класс:[/font]
analysis-console-info-class-value = [font="Monospace" size=11]{$class}[/font]
analysis-console-info-locked = [font="Monospace" size=11]Статус:[/font]
analysis-console-info-locked-value = [font="Monospace" size=11][color={ $state ->
    [0] red]Заблокирован
    [1] lime]Разблокирован
    *[2] plum]Активирован
}[/color][/font]
analysis-console-info-durability = [font="Monospace" size=11]Прочность:[/font]
analysis-console-info-durability-value = [font="Monospace" size=11][color={$color}]{$current}/{$max}[/color][/font]
analysis-console-info-effect = [font="Monospace" size=11]Эффект:[/font]
analysis-console-info-effect-value = [font="Monospace" size=11][color=gray]{ $state ->
    [true] {$info}
    *[false] Разблокируйте узлы для получения информации.
}[/color][/font]
analysis-console-info-trigger = [font="Monospace" size=11]Триггеры:[/font]
analysis-console-info-triggered-value = [font="Monospace" size=11][color=gray]{$triggers}[/color][/font]
analysis-console-info-scanner = Сканирование...
analysis-console-info-scanner-paused = Приостановлено.
analysis-console-progress-text = {$seconds ->
    [one] T-{$seconds} секунда
    *[other] T-{$seconds} секунд
}

analysis-console-extract-value = [font="Monospace" size=11][color=orange]Узел {$id} (+{$value})[/color][/font]
analysis-console-extract-none = [font="Monospace" size=11][color=orange] Ни у одного из разблокированных узлов не осталось очков для извлечения [/color][/font]
analysis-console-extract-sum = [font="Monospace" size=11][color=orange] [/color][/font]

analyzer-artifact-extract-popup = Энергия переливается на поверхности артефакта!